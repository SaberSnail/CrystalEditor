using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CrystalDuelingEngine.Serialization;
using Newtonsoft.Json.Linq;

namespace CrystalEditor.Serialization
{
	public class JsonDeserializer : IDeserializer
	{
		public static ISerializable Deserialize(string text)
		{
			var jsonObject = JObject.Parse(text);
			var deserializer = new JsonDeserializer(jsonObject);
			return SerializationManager.Deserialize(deserializer);
		}

		public JsonDeserializer(JObject jsonObject)
		{
			JsonObject = jsonObject;
		}

		public T GetValue<T>(string key)
		{
			JToken value = JsonObject.GetValue(key);
			if (value == null)
				return default(T);
			return GetOrDeserializeValue<T>(value);
		}

		public IEnumerable<T> GetValues<T>(string key)
		{
			JToken value = JsonObject.GetValue(key);
			if (value == null || value.Type == JTokenType.Null)
				return null;
			return value.Children().Select(GetOrDeserializeValue<T>);
		}

		private T GetOrDeserializeValue<T>(JToken token)
		{
			if (token.Type == JTokenType.Object)
			{
				var childDeserializer = new JsonDeserializer((JObject) token);
				return (T) SerializationManager.Deserialize(childDeserializer);
			}
			if (token.Type == JTokenType.Array)
			{
				var argType = typeof(T).GetGenericArguments()[0];
				var getOrDeserializeValue = GetType().GetMethod(nameof(GetOrDeserializeValue), BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(argType);
				var cast = typeof(Enumerable).GetMethod("Cast", BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(argType);
				var toList = typeof(Enumerable).GetMethod("ToList", BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(argType);
				return (T) toList.Invoke(null, new [] { cast.Invoke(null, new [] { token.Children().Select(x => getOrDeserializeValue.Invoke(this, new object[] { x }))})});
			}
			if (typeof(T).IsEnum)
				return (T) Enum.Parse(typeof(T), token.Value<string>());
			return token.Value<T>();
		}

		private JObject JsonObject { get; }
	}
}
