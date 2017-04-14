using System;
using System.Windows;
using CrystalEditor.MainWindow;

namespace CrystalEditor
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static new App Current
		{
			get { return (App) Application.Current; }
		}

		public App()
		{
			m_appModel = new AppModel();
			m_appModel.OnStartupFinished += AppModel_OnStartupFinished;
		}

		public AppModel AppModel
		{
			get { return m_appModel; }
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			m_appModel.Startup();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			m_appModel.Shutdown();

			base.OnExit(e);
		}

		private void AppModel_OnStartupFinished(object sender, EventArgs eventArgs)
		{
			new MainWindowView(m_appModel.MainWindowViewModel).Show();
		}

		readonly AppModel m_appModel;
	}
}
