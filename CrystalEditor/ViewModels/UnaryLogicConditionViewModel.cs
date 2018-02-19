using System;
using CrystalDuelingEngine.Conditions;

namespace CrystalEditor.ViewModels
{
	public sealed class UnaryLogicConditionViewModel : ConditionViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public ConditionViewModelBase Child
		{
			get
			{
				VerifyAccess();
				return m_child;
			}
			set
			{
				SetPropertyField(nameof(Child), value, ref m_child);
			}
		}

		public UnaryLogicKind LogicKind
		{
			get
			{
				VerifyAccess();
				return m_logicKind;
			}
			set
			{
				SetPropertyField(nameof(LogicKind), value, ref m_logicKind);
			}
		}

		public override ConditionBase ToConditionBase()
		{
			var child = Child.ToConditionBase();
			switch (m_logicKind)
			{
			case UnaryLogicKind.Not:
				return new NotCondition(child);
			default:
				throw new NotImplementedException($"Can not convert '{m_logicKind}' logic kind to ConditionBase.");
			}
		}

		private new static ConditionViewModelBase CreateFromData(ConditionBase condition)
		{
			if (!(condition is UnaryLogicCondition logicCondition))
				return null;

			UnaryLogicKind? kind = null;
			if (condition is NotCondition)
				kind = UnaryLogicKind.Not;
			if (kind == null)
				return null;

			var child = ConditionViewModelBase.CreateFromData(logicCondition.Child);
			return new UnaryLogicConditionViewModel(child, kind.Value);
		}

		private UnaryLogicConditionViewModel(ConditionViewModelBase child, UnaryLogicKind logicKind)
		{
			m_child = child;
			m_logicKind = logicKind;
		}

		ConditionViewModelBase m_child;
		UnaryLogicKind m_logicKind;
	}
}
