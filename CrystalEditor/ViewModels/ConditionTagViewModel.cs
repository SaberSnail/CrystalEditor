using CrystalDuelingEngine.Tags;

namespace CrystalEditor.ViewModels
{
	public sealed class ConditionTagViewModel : ConditionTagViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public override TagBase ToTagBase()
		{
			var condition = Condition.ToConditionBase();
			return new ConditionTag(Key, condition, Duration);
		}

		private new static TagViewModelBase CreateFromData(TagBase tag)
		{
			var conditionTag = tag as ConditionTag;
			if (conditionTag == null)
				return null;

			var condition = ConditionViewModelBase.CreateFromData(conditionTag.Condition);
			return new ConditionTagViewModel(condition, conditionTag.Key, conditionTag.Duration);
		}

		private ConditionTagViewModel(ConditionViewModelBase condition, string key, int? duration)
			: base(condition, key, duration)
		{
		}
	}
}
