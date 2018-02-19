using System.Collections.Generic;
using System.Linq;
using CrystalDuelingEngine.Rules;
using CrystalEditor.ViewModels;

namespace CrystalEditor.GameRulesView
{
	public sealed class GameRulesViewModel : ViewModelBase
	{
		public static  GameRulesViewModel CreateFromData(GameRules rules)
		{
			var preBattleEffects = rules.PreBattleEffects.Select(EffectViewModelBase.CreateFromData);
			var postBattleEffects = rules.PostBattleEffects.Select(EffectViewModelBase.CreateFromData);
			var postTurnEffects = rules.PostTurnEffects.Select(EffectViewModelBase.CreateFromData);
			var preResultEffects = rules.PreResultEffects.Select(EffectViewModelBase.CreateFromData);
			var postResultEffects = rules.PostResultEffects.Select(EffectViewModelBase.CreateFromData);
			var eliminationCondition = ConditionViewModelBase.CreateFromData(rules.EliminationCondition);
			return new GameRulesViewModel(preBattleEffects, postBattleEffects, postTurnEffects, preResultEffects, postResultEffects, eliminationCondition);
		}

		public LabelledViewModelBase SelectedEffect
		{
			get
			{
				VerifyAccess();
				return m_selectedEffect;
			}
			set
			{
				if (value is EffectViewModelBase)
					SetPropertyField(nameof(SelectedEffect), value, ref m_selectedEffect);
				else
					SetPropertyField(nameof(SelectedEffect), null, ref m_selectedEffect);
			}
		}

		public IReadOnlyList<TreeNodeViewModelBase> Effects { get; }

		public ConditionViewModelBase EliminationCondition
		{
			get
			{
				VerifyAccess();
				return m_eliminationCondition;
			}
			set
			{
				SetPropertyField(nameof(EliminationCondition), value, ref m_eliminationCondition);
			}
		}

		public GameRules ToGameRules()
		{
			var rules = new GameRules
			{
				PreBattleEffects = m_preBattleEffectsNode.GetChildren().Select(x => x.ToEffectBase()).ToList().AsReadOnly(),
				PostBattleEffects = m_postBattleEffectsNode.GetChildren().Select(x => x.ToEffectBase()).ToList().AsReadOnly(),
				PostTurnEffects = m_postTurnEffectsNode.GetChildren().Select(x => x.ToEffectBase()).ToList().AsReadOnly(),
				PreResultEffects = m_preResultEffectsNode.GetChildren().Select(x => x.ToEffectBase()).ToList().AsReadOnly(),
				PostResultEffects = m_postResultEffectsNode.GetChildren().Select(x => x.ToEffectBase()).ToList().AsReadOnly(),
				EliminationCondition = m_eliminationCondition.ToConditionBase()
			};
			return rules;
		}

		private GameRulesViewModel(IEnumerable<EffectViewModelBase> preBattleEffects, IEnumerable<EffectViewModelBase> postBattleEffects, IEnumerable<EffectViewModelBase> postTurnEffects, IEnumerable<EffectViewModelBase> preResultEffects, IEnumerable<EffectViewModelBase> postResultEffects, ConditionViewModelBase eliminationCondition)
		{
			m_eliminationCondition = eliminationCondition;
			m_selectedEffect = new TreeNodeViewModel<EffectViewModelBase>("", Enumerable.Empty<EffectViewModelBase>());

			m_preBattleEffectsNode = new TreeNodeViewModel<EffectViewModelBase>(OurResources.PreBattleEffectsNodeLabel, preBattleEffects);
			m_preResultEffectsNode = new TreeNodeViewModel<EffectViewModelBase>(OurResources.PreResultEffectsNodeLabel, preResultEffects);
			m_postResultEffectsNode = new TreeNodeViewModel<EffectViewModelBase>(OurResources.PostResultEffectsNodelLabel, postResultEffects);
			m_postTurnEffectsNode = new TreeNodeViewModel<EffectViewModelBase>(OurResources.PostTurnEffectsNodeLabel, postTurnEffects);
			m_postBattleEffectsNode = new TreeNodeViewModel<EffectViewModelBase>(OurResources.PostBattleEffectsNodeLabel, postBattleEffects);
			Effects = new List<TreeNodeViewModelBase>
			{
				m_preBattleEffectsNode,
				m_preResultEffectsNode,
				m_postResultEffectsNode,
				m_postTurnEffectsNode,
				m_postBattleEffectsNode,
			};
		}

		readonly TreeNodeViewModel<EffectViewModelBase> m_preBattleEffectsNode;
		readonly TreeNodeViewModel<EffectViewModelBase> m_preResultEffectsNode;
		readonly TreeNodeViewModel<EffectViewModelBase> m_postResultEffectsNode;
		readonly TreeNodeViewModel<EffectViewModelBase> m_postTurnEffectsNode;
		readonly TreeNodeViewModel<EffectViewModelBase> m_postBattleEffectsNode;
		ConditionViewModelBase m_eliminationCondition;
		LabelledViewModelBase m_selectedEffect;
	}
}
