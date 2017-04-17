namespace CrystalEditor.ViewModels
{
	public abstract class ConditionTagViewModelBase : TagViewModelBase
	{
		public ConditionViewModelBase Condition
		{
			get
			{
				VerifyAccess();
				return m_condition;
			}
			set
			{
				SetPropertyField(nameof(Condition), value, ref m_condition);
			}
		}

		protected ConditionTagViewModelBase(ConditionViewModelBase condition, string key, int? duration)
			: base(key, duration)
		{
			m_condition = condition;
		}

		ConditionViewModelBase m_condition;
	}
}
