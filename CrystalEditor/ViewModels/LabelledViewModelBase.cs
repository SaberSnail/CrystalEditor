using System;
using System.Windows.Threading;

namespace CrystalEditor.ViewModels
{
	public abstract class LabelledViewModelBase : ViewModelBase
	{
		protected LabelledViewModelBase()
		{
			m_refreshLabelTimer = new DispatcherTimer(DispatcherPriority.Normal, Dispatcher)
			{
				Interval = TimeSpan.FromMilliseconds(100)
			};
			m_refreshLabelTimer.Tick += RefreshLabelNow;
			m_label = new Lazy<string>(GetLabel);
		}

		public string Label => m_label.Value;

		protected abstract string GetLabel();

		protected void RefreshLabelSoon()
		{
			m_refreshLabelTimer.Stop();
			m_refreshLabelTimer.Start();
		}

		private void RefreshLabelNow(object sender, EventArgs e)
		{
			m_refreshLabelTimer.Stop();
			using (ScopedPropertyChange(nameof(Label)))
				m_label = new Lazy<string>(GetLabel);
		}

		readonly DispatcherTimer m_refreshLabelTimer;
		Lazy<string> m_label;
	}
}
