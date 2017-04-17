using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CrystalDuelingEngine.Tags;

namespace CrystalEditor.ViewModels
{
	public sealed class ConditionalTagsTagViewModel : ConditionTagViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public ObservableCollection<TagViewModelBase> Tags { get; }

		public override TagBase ToTagBase()
		{
			var condition = Condition.ToConditionBase();
			var tags = Tags.Select(x => x.ToTagBase());
			return new ConditionalTagsTag(Key, condition, tags, Duration);
		}

		private new static TagViewModelBase CreateFromData(TagBase tag)
		{
			var conditionalTagsTag = tag as ConditionalTagsTag;
			if (conditionalTagsTag == null)
				return null;

			var tags = conditionalTagsTag.Tags.Select(x => TagViewModelBase.CreateFromData(tag));
			var condition = ConditionViewModelBase.CreateFromData(conditionalTagsTag.Condition);
			return new ConditionalTagsTagViewModel(tags, condition, conditionalTagsTag.Key, conditionalTagsTag.Duration);
		}

		private ConditionalTagsTagViewModel(IEnumerable<TagViewModelBase> tags, ConditionViewModelBase condition, string key, int? duration)
			: base(condition, key, duration)
		{
			Tags = new ObservableCollection<TagViewModelBase>(tags);
		}
	}
}
