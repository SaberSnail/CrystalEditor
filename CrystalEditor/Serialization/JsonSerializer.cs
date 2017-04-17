using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CrystalDuelingEngine.Serialization;
using Newtonsoft.Json;

namespace CrystalEditor.Serialization
{
	public class JsonSerializer : ISerializer
	{
		public static string Serialize(ISerializable serializable)
		{
			var jsonSerializer = new Newtonsoft.Json.JsonSerializer();
			StringBuilder builder = new StringBuilder();
			StringWriter writer = new StringWriter(builder);
			using (var jsonWriter = new JsonTextWriter(writer))
			{
				jsonWriter.Formatting = Formatting.Indented;
				var serializer = new JsonSerializer(jsonWriter, jsonSerializer);
				serializable.Serialize(serializer);
			}
			return builder.ToString();
		}

		public JsonSerializer(JsonWriter writer, Newtonsoft.Json.JsonSerializer serializer)
		{
			ObjectStack = new Stack<ISerializable>();
			JsonWriter = writer;
			Serializer = serializer;
		}

		public void StartObject(ISerializable obj)
		{
			bool isSameAsPreviousObject = IsObjectSameAsPreviousObject(obj);
			ObjectStack.Push(obj);
			if (!isSameAsPreviousObject)
			{
				JsonWriter.WriteStartObject();
				SerializationManager.StartObjectSerialization(this, obj);
			}
		}

		public void EndObject()
		{
			var obj = ObjectStack.Pop();
			if (!IsObjectSameAsPreviousObject(obj))
				JsonWriter.WriteEndObject();
		}

		private bool IsObjectSameAsPreviousObject(ISerializable obj)
		{
			return ObjectStack.Count != 0 && ReferenceEquals(obj, ObjectStack.Peek());
		}

		public void SerializeValue(string key, object value)
		{
			JsonWriter.WritePropertyName(key);
			WritePropertyValue(value);
		}

		private void WritePropertyValue(object value)
		{
			var type = value?.GetType();
			if (value == null)
			{
				Serializer.Serialize(JsonWriter, null);
			}
			else if (type.IsEnum)
			{
				Serializer.Serialize(JsonWriter, value.ToString());
			}
			else if (IsSimpleType(type))
			{
				Serializer.Serialize(JsonWriter, value);
			}
			else if (value is ISerializable)
			{
				((ISerializable) value).Serialize(this);
			}
			else if (value is IEnumerable)
			{
				JsonWriter.WriteStartArray();
				foreach (var singleValue in (IEnumerable) value)
					WritePropertyValue(singleValue);
				JsonWriter.WriteEndArray();
			}
			else
			{
				throw new SerializationFormatException($"Serialized object must be IEnumerable, ISerializable, or simple: {value.GetType().FullName}");
			}
		}

		private bool IsSimpleType(Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
				return IsSimpleType(type.GetGenericArguments()[0]);

			return type.IsPrimitive
			  || type.IsEnum
			  || type == typeof(string)
			  || type == typeof(decimal);
		}

		private Stack<ISerializable> ObjectStack { get; }

		private JsonWriter JsonWriter { get; }

		private Newtonsoft.Json.JsonSerializer Serializer { get; }
	}
}
