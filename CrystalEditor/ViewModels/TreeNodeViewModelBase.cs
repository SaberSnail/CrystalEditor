using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CrystalEditor.ViewModels
{
	public sealed class TreeNodeViewModel<T> : TreeNodeViewModelBase where T : LabelledViewModelBase
	{
		public TreeNodeViewModel(string label, IEnumerable<T> children)
			: base(label, children)
		{ }

		public IReadOnlyList<EffectViewModelBase> GetChildren() => Children.Cast<EffectViewModelBase>().ToList();
	}

	public abstract class TreeNodeViewModelBase : LabelledViewModelBase
	{
		public TreeNodeViewModelBase(string label, IEnumerable<LabelledViewModelBase> children)
		{
			Children = new ObservableCollection<LabelledViewModelBase>(children);
			m_label = label;
		}

		public ObservableCollection<LabelledViewModelBase> Children { get; }

		protected override string GetLabel() => m_label;

		private readonly string m_label;
	}
}
