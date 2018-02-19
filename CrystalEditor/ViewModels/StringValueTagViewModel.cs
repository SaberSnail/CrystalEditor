using CrystalDuelingEngine.Tags;

namespace CrystalEditor.ViewModels
{
	public sealed class StringValueTagViewModel : ValueTagViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public string Value
		{
			get
			{
				VerifyAccess();
				return m_value;
			}
			set
			{
				SetPropertyField(nameof(Value), value, ref m_value);
			}
		}

		public override TagBase ToTagBase()
		{
			return new StringValueTag(Key, Value, Duration);
		}

		private new static TagViewModelBase CreateFromData(TagBase tag)
		{
			if (!(tag is StringValueTag stringValueTag))
				return null;

			return new StringValueTagViewModel(stringValueTag.Value, stringValueTag.Key, stringValueTag.Duration);
		}

		private StringValueTagViewModel(string value, string key, int? duration)
			: base(key, duration)
		{
			Value = value;
		}

		string m_value;
	}
}
