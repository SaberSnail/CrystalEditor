using System;
using System.Collections.Generic;
using CrystalDuelingEngine.Tags;

namespace CrystalEditor.ViewModels
{
	public abstract class TagViewModelBase : ViewModelBase
	{
		public static TagViewModelBase CreateFromData(TagBase tag)
		{
			foreach (var factory in s_factories)
			{
				var viewModel = factory(tag);
				if (viewModel != null)
					return viewModel;
			}
			throw new NotImplementedException($"No Tag view model has been registered for '{tag.GetType().Name}'.");
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
				SetPropertyField(nameof(Key), value, ref m_key);
			}
		}

		public int? Duration
		{
			get
			{
				VerifyAccess();
				return m_duration;
			}
			set
			{
				SetPropertyField(nameof(Duration), value, ref m_duration);
			}
		}

		public abstract TagBase ToTagBase();

		protected static void RegisterViewModelFactory(Func<TagBase, TagViewModelBase> factory)
		{
			s_factories.Add(factory);
		}

		protected TagViewModelBase(string key, int? duration)
		{
			m_key = key;
			m_duration = duration;
		}

		static readonly List<Func<TagBase, TagViewModelBase>> s_factories = new List<Func<TagBase, TagViewModelBase>>();

		string m_key;
		int? m_duration;
	}
}
