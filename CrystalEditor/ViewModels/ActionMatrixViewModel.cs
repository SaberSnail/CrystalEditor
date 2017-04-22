using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CrystalDuelingEngine;

namespace CrystalEditor.ViewModels
{
	public sealed class ActionMatrixViewModel : ViewModelBase
	{
		public static ActionMatrixViewModel CreateFromData(IEnumerable<ActionMatrixEntry> entries)
		{
			var entryViewModels = entries.Select(ActionMatrixEntryViewModel.CreateFromData);
			return new ActionMatrixViewModel(entryViewModels);
		}

		public ObservableCollection<ActionMatrixEntryViewModel> Entries { get; }

		public IEnumerable<ActionMatrixEntry> ToActionMatrixEntries()
		{
			return Entries.Select(x => x.ToActionMatrixEntry());
		}

		private ActionMatrixViewModel(IEnumerable<ActionMatrixEntryViewModel> entries)
		{
			Entries = new ObservableCollection<ActionMatrixEntryViewModel>(entries);
		}
	}
}
