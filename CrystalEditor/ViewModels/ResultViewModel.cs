using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using CrystalDuelingEngine;

namespace CrystalEditor.ViewModels
{
	public sealed class ResultViewModel : LabeledViewModelBase
	{
		public static ResultViewModel CreateFromData(Result result, StringLookupViewModel stringLookup)
		{
			var effects = result.Effects.Select(EffectViewModelBase.CreateFromData);
			var tags = result.Tags.Select(TagViewModelBase.CreateFromData);
			return new ResultViewModel(result.Name, result.Key, effects, tags, stringLookup);
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

		public ObservableCollection<EffectViewModelBase> Effects { get; }

		public StringLookupViewModel StringLookup { get; }

		public Result ToResult()
		{
			var effects = Effects.Select(x => x.ToEffectBase());
			var tags = Tags.Select(x => x.ToTagBase());
			return new Result(Key, Name, effects, tags);
		}

		protected override string GetLabel()
		{
			if (m_key is null)
				return OurResources.NewResultLabel;

			return string.Format(CultureInfo.CurrentCulture, OurResources.ResultLabel, m_key, RenderedName);
		}

		private ResultViewModel(string name, string key, IEnumerable<EffectViewModelBase> effects, IEnumerable<TagViewModelBase> tags, StringLookupViewModel stringLookup)
		{
			m_name = name;
			m_key = key;
			Effects = new ObservableCollection<EffectViewModelBase>(effects);
			Tags = new ObservableCollection<TagViewModelBase>(tags);
			StringLookup = stringLookup;
		}

		string m_name;
		string m_key;
	}
}