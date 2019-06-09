using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CrystalEditor.ViewModels
{
	public sealed class TreeNodeViewModel<T> : TreeNodeViewModelBase where T : LabeledViewModelBase
	{
		public TreeNodeViewModel(string label, IEnumerable<T> children)
			: base(label, children)
		{ }

		public IReadOnlyList<T> GetChildren() => Children.Cast<T>().ToList();
	}

	public abstract class TreeNodeViewModelBase : LabeledViewModelBase
	{
		public TreeNodeViewModelBase(string label, IEnumerable<LabeledViewModelBase> children)
		{
			Children = new ObservableCollection<LabeledViewModelBase>(children);
			m_label = label;
		}

		public ObservableCollection<LabeledViewModelBase> Children { get; }

		protected override string GetLabel() => m_label;

		private readonly string m_label;
	}
}
