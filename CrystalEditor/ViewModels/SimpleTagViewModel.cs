using CrystalDuelingEngine.Tags;

namespace CrystalEditor.ViewModels
{
	public sealed class SimpleTagViewModel : TagViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public override TagBase ToTagBase()
		{
			return new SimpleTag(Key, Duration);
		}

		private new static TagViewModelBase CreateFromData(TagBase tag)
		{
			var simpleTag = tag as SimpleTag;
			if (simpleTag == null)
				return null;

			return new SimpleTagViewModel(simpleTag.Key, simpleTag.Duration);
		}

		private SimpleTagViewModel(string key, int? duration)
			: base(key, duration)
		{
		}
	}
}
