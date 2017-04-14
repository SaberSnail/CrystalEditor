using System;
using System.Windows.Threading;
using GoldenAnvil.Utility;

namespace CrystalEditor.ViewModels
{
	public abstract class ViewModelBase : NotifyPropertyChangedBase
	{
		protected ViewModelBase()
		{
			m_dispatcher = Dispatcher.CurrentDispatcher;
		}

		protected AppModel AppModel => AppModel.Current;

		protected void VerifyAccess()
		{
			if (Dispatcher.CurrentDispatcher != m_dispatcher)
				throw new InvalidOperationException("Code must be run on the same thread as this object was constructed.");
		}

		readonly Dispatcher m_dispatcher;
	}
}
