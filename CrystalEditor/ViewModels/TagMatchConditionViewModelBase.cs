using CrystalDuelingEngine;

namespace CrystalEditor.ViewModels
{
	public abstract class TagMatchConditionViewModelBase : ConditionViewModelBase
	{
		public TagScope MatchScopes
		{
			get
			{
				VerifyAccess();
				return m_matchScopes;
			}
			set
			{
				SetPropertyField(value, ref m_matchScopes);
			}
		}

		public string MatchKey
		{
			get
			{
				VerifyAccess();
				return m_matchKey;
			}
			set
			{
				SetPropertyField(value, ref m_matchKey);
			}
		}

		public MatchKind KeyMatchKind
		{
			get
			{
				VerifyAccess();
				return m_keyMatchKind;
			}
			set
			{
				SetPropertyField(value, ref m_keyMatchKind);
			}
		}

		protected TagMatchConditionViewModelBase(TagScope matchScopes, string matchKey, MatchKind keyMatchKind)
		{
			m_matchScopes = matchScopes;
			m_matchKey = matchKey;
			m_keyMatchKind = keyMatchKind;
		}

		TagScope m_matchScopes;
		string m_matchKey;
		MatchKind m_keyMatchKind;
	}
}
