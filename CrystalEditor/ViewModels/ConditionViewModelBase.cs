using System;
using System.Collections.Generic;
using CrystalDuelingEngine.Conditions;

namespace CrystalEditor.ViewModels
{
	public abstract class ConditionViewModelBase : ViewModelBase
	{
		public static ConditionViewModelBase CreateFromData(ConditionBase condition)
		{
			foreach (var factory in s_factories)
			{
				var viewModel = factory(condition);
				if (viewModel != null)
					return viewModel;
			}
			throw new NotImplementedException($"No Condition view model has been registered for '{condition.GetType().Name}'.");
		}

		public abstract ConditionBase ToConditionBase();

		protected static void RegisterViewModelFactory(Func<ConditionBase, ConditionViewModelBase> factory)
		{
			s_factories.Add(factory);
		}

		static readonly List<Func<ConditionBase, ConditionViewModelBase>> s_factories = new List<Func<ConditionBase, ConditionViewModelBase>>();
	}
}
