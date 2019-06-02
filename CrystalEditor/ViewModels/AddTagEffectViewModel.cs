using System.Globalization;
using CrystalDuelingEngine;
using CrystalDuelingEngine.Effects;

namespace CrystalEditor.ViewModels
{
	public sealed class AddTagEffectViewModel : EffectViewModelBase
	{
		public static void RegisterFactory()
		{
			RegisterViewModelFactory(CreateFromData);
		}

		public TagViewModelBase Tag
		{
			get
			{
				VerifyAccess();
				return m_tag;
			}
			set
			{
				if (SetPropertyField(value, ref m_tag))
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
				SetPropertyField(value, ref m_conflictResolution);
			}
		}

		public override EffectBase ToEffectBase()
		{
			var tag = m_tag.ToTagBase();
			var condition = Condition.ToConditionBase();
			return new AddTagEffect(tag, m_conflictResolution, condition, Target);
		}

		protected override string GetLabel()
		{
			if (m_tag == null)
				return OurResources.NewAddTagLabel;
			return string.Format(CultureInfo.CurrentCulture, OurResources.AddTagLabel, m_tag.Label);
		}

		private new static EffectViewModelBase CreateFromData(EffectBase effect)
		{
			if (!(effect is AddTagEffect addTagEffect))
				return null;

			var tag = TagViewModelBase.CreateFromData(addTagEffect.Tag);
			var condition = ConditionViewModelBase.CreateFromData(addTagEffect.Condition);
			return new AddTagEffectViewModel(tag, addTagEffect.ConflictResolution, condition, addTagEffect.Target);
		}

		private AddTagEffectViewModel(TagViewModelBase tag, KeyConflictResolutionKind conflictResolution, ConditionViewModelBase condition, EffectTarget target)
			: base(condition, target)
		{
			m_tag = tag;
			m_conflictResolution = conflictResolution;
		}

		TagViewModelBase m_tag;
		KeyConflictResolutionKind m_conflictResolution;
	}
}