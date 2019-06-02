using CrystalDuelingEngine.Tags;

namespace CrystalEditor.ViewModels
{
	public sealed class IntValueTagViewModel : ValueTagViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public int? Value
		{
			get
			{
				VerifyAccess();
				return m_value;
			}
			set
			{
				SetPropertyField(value, ref m_value);
			}
		}

		public override TagBase ToTagBase()
		{
			return new IntValueTag(Key, Value, Duration);
		}

		private new static TagViewModelBase CreateFromData(TagBase tag)
		{
			if (!(tag is IntValueTag intValueTag))
				return null;

			return new IntValueTagViewModel(intValueTag.Value, intValueTag.Key, intValueTag.Duration);
		}

		private IntValueTagViewModel(int? value, string key, int? duration)
			: base(key, duration)
		{
			Value = value;
		}

		int? m_value;
	}
}
