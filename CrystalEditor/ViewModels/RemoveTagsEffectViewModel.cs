using CrystalDuelingEngine;
using CrystalDuelingEngine.Effects;

namespace CrystalEditor.ViewModels
{
	public sealed class RemoveTagsEffectViewModel : EffectViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public string TagKey
		{
			get
			{
				VerifyAccess();
				return m_tagKey;
			}
			set
			{
				SetPropertyField(nameof(TagKey), value, ref m_tagKey);
			}
		}

		public MatchKind TagKeyMatchKind
		{
			get
			{
				VerifyAccess();
				return m_tagKeyMatchKind;
			}
			set
			{
				SetPropertyField(nameof(TagKeyMatchKind), value, ref m_tagKeyMatchKind);
			}
		}

		public override EffectBase ToEffectBase()
		{
			var condition = Condition.ToConditionBase();
			var effect = new RemoveTagsEffect(m_tagKey, m_tagKeyMatchKind, condition, Target);
			return effect;
		}

		private new static EffectViewModelBase CreateFromData(EffectBase effect)
		{
			var removeTagsEffect = effect as RemoveTagsEffect;
			if (removeTagsEffect == null)
				return null;

			var condition = ConditionViewModelBase.CreateFromData(removeTagsEffect.Condition);
			return new RemoveTagsEffectViewModel(removeTagsEffect.TagKey, removeTagsEffect.TagKeyMatchKind, condition, removeTagsEffect.Target);
		}

		private RemoveTagsEffectViewModel(string tagKey, MatchKind tagKeyMatchKind, ConditionViewModelBase condition, EffectTarget target)
			: base(condition, target)
		{
			m_tagKey = tagKey;
			m_tagKeyMatchKind = tagKeyMatchKind;
		}

		string m_tagKey;
		MatchKind m_tagKeyMatchKind;
	}
}
