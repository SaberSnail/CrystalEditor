using CrystalDuelingEngine;
using CrystalDuelingEngine.Conditions;

namespace CrystalEditor.ViewModels
{
	public sealed class HasTagConditionViewModel : TagMatchConditionViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public override ConditionBase ToConditionBase()
		{
			return new HasTagCondition(MatchScopes, MatchKey, KeyMatchKind);
		}

		private new static ConditionViewModelBase CreateFromData(ConditionBase condition)
		{
			if (!(condition is HasTagCondition hasTagCondition))
				return null;

			return new HasTagConditionViewModel(hasTagCondition.MatchScopes, hasTagCondition.MatchKey, hasTagCondition.KeyMatchKind);
		}

		private HasTagConditionViewModel(TagScope matchScopes, string matchKey, MatchKind keyMatchKind)
			: base(matchScopes, matchKey, keyMatchKind)
		{
		}
	}
}
