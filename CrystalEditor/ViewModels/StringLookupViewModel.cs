using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CrystalDuelingEngine;

namespace CrystalEditor.ViewModels
{
	public sealed class StringLookupViewModel : ViewModelBase
	{
		public static StringLookupViewModel CreateFromData(StringLookup lookup)
		{
			return new StringLookupViewModel(lookup.Values.Select(x => new StringLookupItemViewModel(x.Item1, x.Item2)));
		}

		public ObservableCollection<StringLookupItemViewModel> Items { get; }

		public string Lookup(string key)
		{
			return Items.FirstOrDefault(x => x.Key == key)?.Value;
		}

		public StringLookup ToStringLookup()
		{
			return new StringLookup(Items.Select(x => new Tuple<string, string>(x.Key, x.Value)));
		}

		private StringLookupViewModel(IEnumerable<StringLookupItemViewModel> items)
		{
			Items = new ObservableCollection<StringLookupItemViewModel>(items);
		}
	}
}
