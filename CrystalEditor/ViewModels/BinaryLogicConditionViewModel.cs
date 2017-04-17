using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CrystalDuelingEngine.Conditions;

namespace CrystalEditor.ViewModels
{
	public sealed class BinaryLogicConditionViewModel : ConditionViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public ObservableCollection<ConditionViewModelBase> Children { get; }

		public BinaryLogicKind LogicKind
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
			var children = Children.Select(x => x.ToConditionBase());
			switch (m_logicKind)
			{
			case BinaryLogicKind.And:
				return new AndCondition(children);
			case BinaryLogicKind.Or:
				return new OrCondition(children);
			default:
				throw new NotImplementedException($"Can not convert '{m_logicKind}' logic kind to ConditionBase.");
			}
		}

		private new static ConditionViewModelBase CreateFromData(ConditionBase condition)
		{
			BinaryLogicKind? kind = null;
			BinaryLogicCondition logicCondition = condition as AndCondition;
			if (logicCondition != null)
			{
				kind = BinaryLogicKind.And;
			}
			else
			{
				logicCondition = condition as OrCondition;
				if (logicCondition != null)
					kind = BinaryLogicKind.Or;
			}
			if (logicCondition == null)
				return null;

			var children = logicCondition.Children.Select(ConditionViewModelBase.CreateFromData);
			return new BinaryLogicConditionViewModel(children, kind.Value);
		}

		private BinaryLogicConditionViewModel(IEnumerable<ConditionViewModelBase> children, BinaryLogicKind logicKind)
		{
			Children = new ObservableCollection<ConditionViewModelBase>(children);
			m_logicKind = logicKind;
		}

		BinaryLogicKind m_logicKind;
	}
}
