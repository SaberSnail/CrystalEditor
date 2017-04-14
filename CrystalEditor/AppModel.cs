using System;
using System.Windows;
using CrystalEditor.MainWindow;
using GoldenAnvil.Utility;

namespace CrystalEditor
{
	public sealed class AppModel
	{
		public static string OrganizationName
		{
			get { return "AStott Productions"; }
		}

		public static string ApplicationName
		{
			get { return "Crystal Editor"; }
		}

		public static AppModel Current
		{
			get
			{
				return ((App) Application.Current).AppModel;
			}
		}

		public AppModel()
		{
		}

		public event EventHandler OnStartupFinished;

		public MainWindowViewModel MainWindowViewModel { get; set; }

		public void Startup()
		{
			MainWindowViewModel = new MainWindowViewModel();
			OnStartupFinished.Raise(this);
		}

		public void Shutdown()
		{
		}
	}
}
