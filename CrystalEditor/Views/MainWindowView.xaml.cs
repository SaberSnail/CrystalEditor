using System.Windows;
using CrystalEditor.ViewModels;

namespace CrystalEditor.Views
{
	/// <summary>
	/// Interaction logic for MainWindowView.xaml
	/// </summary>
	public partial class MainWindowView : Window
	{
		public MainWindowView(MainWindowViewModel viewModel)
		{
			ViewModel = viewModel;

			InitializeComponent();
		}

		public MainWindowViewModel ViewModel { get; }
	}
}
