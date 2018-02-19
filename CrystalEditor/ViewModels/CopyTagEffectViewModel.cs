using System.Globalization;
using CrystalDuelingEngine;
using CrystalDuelingEngine.Effects;

namespace CrystalEditor.ViewModels
{
	public sealed class CopyTagEffectViewModel : EffectViewModelBase
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
				if (SetPropertyField(nameof(TagKey), value, ref m_tagKey))
					RefreshLabelSoon();
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

		public TagScope TagScope
		{
			get
			{
				VerifyAccess();
				return m_tagScope;
			}
			set
			{
				SetPropertyField(nameof(TagScope), value, ref m_tagScope);
			}
		}

		public string NewTagKey
		{
			get
			{
				VerifyAccess();
				return m_newTagKey;
			}
			set
			{
				if (SetPropertyField(nameof(NewTagKey), value, ref m_newTagKey))
					RefreshLabelSoon();
			}
		}

		public KeyConflictResolutionKind ConflictResolution
		{
			get
			{
				VerifyAccess();
				return m_conflictResolution;
			}
			set
			{
				SetPropertyField(nameof(ConflictResolution), value, ref m_conflictResolution);
			}
		}

		public override EffectBase ToEffectBase()
		{
			var condition = Condition.ToConditionBase();
			return new CopyTagsEffect(m_tagKey, m_tagKeyMatchKind, m_tagScope, condition, m_newTagKey, m_conflictResolution, Target);
		}

		protected override string GetLabel()
		{
			if (m_tagKey == null)
				return OurResources.NewCopyTagLabel;
			return string.Format(CultureInfo.CurrentCulture, OurResources.CopyTagLabel, m_tagKey, m_newTagKey ?? "");
		}

		private new static EffectViewModelBase CreateFromData(EffectBase effect)
		{
			var copyTagsEffect = effect as CopyTagsEffect;
			if (copyTagsEffect == null)
				return null;

			var condition = ConditionViewModelBase.CreateFromData(copyTagsEffect.Condition);
			return new CopyTagEffectViewModel(copyTagsEffect.TagKey, copyTagsEffect.TagKeyMatchKind, copyTagsEffect.TagScope, copyTagsEffect.NewTagKey, copyTagsEffect.ConflictResolution, condition, copyTagsEffect.Target);
		}

		private CopyTagEffectViewModel(string tagKey, MatchKind tagKeyMatchKind, TagScope tagScope, string newTagKey, KeyConflictResolutionKind conflictResolution, ConditionViewModelBase condition, EffectTarget target)
			: base(condition, target)
		{
			m_tagKey = tagKey;
			m_tagKeyMatchKind = tagKeyMatchKind;
			m_tagScope = tagScope;
			m_newTagKey = newTagKey;
			m_conflictResolution = conflictResolution;
		}

		string m_tagKey;
		MatchKind m_tagKeyMatchKind;
		TagScope m_tagScope;
		string m_newTagKey;
		KeyConflictResolutionKind m_conflictResolution;
	}
}