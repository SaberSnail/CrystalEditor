using System;
using System.Windows.Threading;
using GoldenAnvil.Utility;

namespace CrystalEditor.ViewModels
{
	public abstract class ViewModelBase : NotifyPropertyChangedBase
	{
		protected ViewModelBase()
		{
			Dispatcher = Dispatcher.CurrentDispatcher;
		}

		protected Dispatcher Dispatcher { get; }

		protected AppModel AppModel => AppModel.Current;

		protected override void VerifyAccess()
		{
			if (Dispatcher.CurrentDispatcher != Dispatcher)
				throw new InvalidOperationException("Code must be run on the same thread as this object was constructed.");
		}
	}
}
