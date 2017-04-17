using System;
using CrystalDuelingEngine;
using CrystalDuelingEngine.Conditions;

namespace CrystalEditor.ViewModels
{
	public sealed class ValueCompareConditionViewModel : TagMatchConditionViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public int? CompareIntValue
		{
			get
			{
				VerifyAccess();
				return m_compareIntValue;
			}
			set
			{
				SetPropertyField(nameof(CompareIntValue), value, ref m_compareIntValue);
			}
		}

		public string CompareStringValue
		{
			get
			{
				VerifyAccess();
				return m_compareStringValue;
			}
			set
			{
				SetPropertyField(nameof(CompareStringValue), value, ref m_compareStringValue);
			}
		}

		public CompareKind CompareKind
		{
			get
			{
				VerifyAccess();
				return m_compareKind;
			}
			set
			{
				SetPropertyField(nameof(CompareKind), value, ref m_compareKind);
			}
		}

		public override ConditionBase ToConditionBase()
		{
			switch (m_compareKind)
			{
			case CompareKind.Equals:
				return m_compareIntValue.HasValue
					? new EqualsValueCondition(MatchScopes, MatchKey, KeyMatchKind, m_compareIntValue.Value)
					: new EqualsValueCondition(MatchScopes, MatchKey, KeyMatchKind, m_compareStringValue);
			case CompareKind.GreaterThanOrEqual:
				return m_compareIntValue.HasValue
					? new GreaterThanOrEqualValueCondition(MatchScopes, MatchKey, KeyMatchKind, m_compareIntValue.Value)
					: new GreaterThanOrEqualValueCondition(MatchScopes, MatchKey, KeyMatchKind, m_compareStringValue);
			case CompareKind.GreaterThan:
				return m_compareIntValue.HasValue
					? new GreaterThanValueCondition(MatchScopes, MatchKey, KeyMatchKind, m_compareIntValue.Value)
					: new GreaterThanValueCondition(MatchScopes, MatchKey, KeyMatchKind, m_compareStringValue);
			case CompareKind.LessThanOrEqual:
				return m_compareIntValue.HasValue
					? new LessThanOrEqualValueCondition(MatchScopes, MatchKey, KeyMatchKind, m_compareIntValue.Value)
					: new LessThanOrEqualValueCondition(MatchScopes, MatchKey, KeyMatchKind, m_compareStringValue);
			case CompareKind.LessThan:
				return m_compareIntValue.HasValue
					? new LessThanValueCondition(MatchScopes, MatchKey, KeyMatchKind, m_compareIntValue.Value)
					: new LessThanValueCondition(MatchScopes, MatchKey, KeyMatchKind, m_compareStringValue);
			default:
				throw new NotImplementedException($"Can not convert '{m_compareKind}' compare kind to ConditionBase.");
			}
		}

		private new static ConditionViewModelBase CreateFromData(ConditionBase condition)
		{
			CompareKind? kind = null;
			ValueCompareCondition compareCondition = condition as EqualsValueCondition;
			if (compareCondition != null)
				kind = CompareKind.Equals;
			if (compareCondition == null)
			{
				compareCondition = condition as GreaterThanOrEqualValueCondition;
				if (compareCondition != null)
					kind = CompareKind.GreaterThanOrEqual;
			}
			if (compareCondition == null)
			{
				compareCondition = condition as GreaterThanValueCondition;
				if (compareCondition != null)
					kind = CompareKind.GreaterThan;
			}
			if (compareCondition == null)
			{
				compareCondition = condition as LessThanOrEqualValueCondition;
				if (compareCondition != null)
					kind = CompareKind.LessThanOrEqual;
			}
			if (compareCondition == null)
			{
				compareCondition = condition as LessThanValueCondition;
				if (compareCondition != null)
					kind = CompareKind.LessThan;
			}
			if (compareCondition == null)
				return null;

			return compareCondition.CompareIntValue.HasValue ?
				new ValueCompareConditionViewModel(kind.Value, compareCondition.CompareIntValue, compareCondition.MatchScopes, compareCondition.MatchKey, compareCondition.KeyMatchKind) :
				new ValueCompareConditionViewModel(kind.Value, compareCondition.CompareStringValue, compareCondition.MatchScopes, compareCondition.MatchKey, compareCondition.KeyMatchKind);
		}

		private ValueCompareConditionViewModel(CompareKind compareKind, int? compareIntValue, TagScope matchScopes, string matchKey, MatchKind keyMatchKind)
			: base(matchScopes, matchKey, keyMatchKind)
		{
			m_compareIntValue = compareIntValue;
			m_compareKind = compareKind;
		}

		private ValueCompareConditionViewModel(CompareKind compareKind, string compareStringValue, TagScope matchScopes, string matchKey, MatchKind keyMatchKind)
			: base(matchScopes, matchKey, keyMatchKind)
		{
			m_compareStringValue = compareStringValue;
			m_compareKind = compareKind;
		}

		int? m_compareIntValue;
		string m_compareStringValue;
		CompareKind m_compareKind;
	}
}
