using System.Windows;
using System.Windows.Controls;
using CrystalEditor.ViewModels;

namespace CrystalEditor.Views
{
	public partial class EntityRulesView : ResourceDictionary
	{
		private void RulesTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			((EntityRulesViewModel) ((TreeView) sender).DataContext).SelectedRule = (LabeledViewModelBase) e.NewValue;
		}
	}
}
