using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CrystalDuelingEngine;

namespace CrystalEditor.ViewModels
{
	public sealed class ActionViewModel : ViewModelBase
	{
		public string Name
		{
			get
			{
				VerifyAccess();
				return m_name;
			}
			set
			{
				SetPropertyField(value, ref m_name);
			}
		}

		public string Key
		{
			get
			{
				VerifyAccess();
				return m_key;
			}
			set
			{
				SetPropertyField(value, ref m_key);
			}
		}

		public ObservableCollection<TagViewModelBase> Tags { get; }

		public Action ToAction()
		{
			var tags = Tags.Select(x => x.ToTagBase());
			return new Action(Key, Name, tags);
		}

		public static ActionViewModel CreateFromData(Action action)
		{
			var tags = action.Tags.Select(TagViewModelBase.CreateFromData);
			return new ActionViewModel(action.Name, action.Key, tags);
		}

		private ActionViewModel(string name, string key, IEnumerable<TagViewModelBase> tags)
		{
			m_name = name;
			m_key = key;
			Tags = new ObservableCollection<TagViewModelBase>(tags);
		}

		string m_name;
		string m_key;
	}
}
