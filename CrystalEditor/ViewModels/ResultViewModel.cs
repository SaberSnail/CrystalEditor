using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CrystalDuelingEngine;

namespace CrystalEditor.ViewModels
{
	public sealed class ResultViewModel : ViewModelBase
	{
		public static ResultViewModel CreateFromData(Result result)
		{
			var effects = result.Effects.Select(EffectViewModelBase.CreateFromData);
			var tags = result.Tags.Select(TagViewModelBase.CreateFromData);
			return new ResultViewModel(result.Name, result.Key, effects, tags);
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

		public ObservableCollection<EffectViewModelBase> Effects { get; }

		public Result ToResult()
		{
			var effects = Effects.Select(x => x.ToEffectBase());
			var tags = Tags.Select(x => x.ToTagBase());
			return new Result(Key, Name, effects, tags);
		}

		private ResultViewModel(string name, string key, IEnumerable<EffectViewModelBase> effects, IEnumerable<TagViewModelBase> tags)
		{
			m_name = name;
			m_key = key;
			Effects = new ObservableCollection<EffectViewModelBase>(effects);
			Tags = new ObservableCollection<TagViewModelBase>(tags);
		}

		string m_name;
		string m_key;
	}
}