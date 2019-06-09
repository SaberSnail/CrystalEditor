using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using CrystalDuelingEngine;

namespace CrystalEditor.ViewModels
{
	public sealed class ActionViewModel : LabeledViewModelBase
	{
		public static ActionViewModel CreateFromData(Action action, StringLookupViewModel stringLookup)
		{
			var tags = action.Tags.Select(TagViewModelBase.CreateFromData);
			return new ActionViewModel(action.Name, action.Key, tags, stringLookup);
		}

		public string Name
		{
			get
			{
				VerifyAccess();
				return m_name;
			}
			set
			{
				if (SetPropertyField(value, ref m_name))
					RefreshLabelSoon();
			}
		}

		public string RenderedName => StringLookup.Lookup(m_name);

		public string Key
		{
			get
			{
				VerifyAccess();
				return m_key;
			}
			set
			{
				if (SetPropertyField(value, ref m_key))
					RefreshLabelSoon();
			}
		}

		public ObservableCollection<TagViewModelBase> Tags { get; }

		public StringLookupViewModel StringLookup { get; }

		public Action ToAction()
		{
			var tags = Tags.Select(x => x.ToTagBase());
			return new Action(Key, Name, tags);
		}

		protected override string GetLabel()
		{
			if (m_key is null)
				return OurResources.NewActionLabel;

			return string.Format(CultureInfo.CurrentCulture, OurResources.ActionLabel, m_key, RenderedName);
		}

		private ActionViewModel(string name, string key, IEnumerable<TagViewModelBase> tags, StringLookupViewModel stringLookup)
		{
			m_name = name;
			m_key = key;
			Tags = new ObservableCollection<TagViewModelBase>(tags);
			StringLookup = stringLookup;
		}

		string m_name;
		string m_key;
	}
}
