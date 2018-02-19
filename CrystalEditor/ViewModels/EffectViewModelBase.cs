using System;
using System.Collections.Generic;
using CrystalDuelingEngine.Effects;

namespace CrystalEditor.ViewModels
{
	public abstract class EffectViewModelBase : LabelledViewModelBase
	{
		public static EffectViewModelBase CreateFromData(EffectBase effect)
		{
			foreach (var factory in s_factories)
			{
				var viewModel = factory(effect);
				if (viewModel != null)
					return viewModel;
			}
			throw new NotImplementedException($"No Effect view model has been registered for '{effect.GetType().Name}'.");
		}

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

		public EffectTarget Target
		{
			get
			{
				VerifyAccess();
				return m_target;
			}
			set
			{
				SetPropertyField(nameof(Target), value, ref m_target);
			}
		}

		public abstract EffectBase ToEffectBase();

		protected static void RegisterViewModelFactory(Func<EffectBase, EffectViewModelBase> factory)
		{
			s_factories.Add(factory);
		}

		protected EffectViewModelBase(ConditionViewModelBase condition, EffectTarget target)
		{
			m_condition = condition;
			m_target = target;
		}

		static readonly List<Func<EffectBase, EffectViewModelBase>> s_factories = new List<Func<EffectBase, EffectViewModelBase>>();

		ConditionViewModelBase m_condition;
		EffectTarget m_target;
	}
}
