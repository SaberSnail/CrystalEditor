using CrystalDuelingEngine.Conditions;

namespace CrystalEditor.ViewModels
{
	public sealed class AlwaysConditionViewModel : ConditionViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public bool Value
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

		public override ConditionBase ToConditionBase()
		{
			return Value ? AlwaysCondition.True : AlwaysCondition.False;
		}

		private new static ConditionViewModelBase CreateFromData(ConditionBase condition)
		{
			if (!(condition is AlwaysCondition alwaysCondition))
				return null;

			return new AlwaysConditionViewModel(alwaysCondition.Value);
		}

		private AlwaysConditionViewModel(bool value)
		{
			m_value = value;
		}

		bool m_value;
	}
}
