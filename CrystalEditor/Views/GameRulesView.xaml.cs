using System.Windows;
using System.Windows.Controls;
using CrystalEditor.ViewModels;

namespace CrystalEditor.Views
{
	public partial class GameRulesView : ResourceDictionary
	{
		private void EffectsTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			((GameRulesViewModel) ((TreeView) sender).DataContext).SelectedEffect = (LabelledViewModelBase) e.NewValue;
		}
	}
}
