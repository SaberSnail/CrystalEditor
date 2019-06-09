using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CrystalDuelingEngine.Rules;

namespace CrystalEditor.ViewModels
{
	public sealed class EntityRulesViewModel : ViewModelBase
	{
		public static EntityRulesViewModel CreateFromData(EntityRules entity)
		{
			var stringLookup = StringLookupViewModel.CreateFromData(entity.StringLookup);
			var preBattleEffects = entity.PreBattleEffects.Select(EffectViewModelBase.CreateFromData);
			var postBattleEffects = entity.PostBattleEffects.Select(EffectViewModelBase.CreateFromData);
			var preTurnEffects = entity.PreTurnEffects.Select(EffectViewModelBase.CreateFromData);
			var postTurnEffects = entity.PostTurnEffects.Select(EffectViewModelBase.CreateFromData);
			var eliminationCondition = ConditionViewModelBase.CreateFromData(entity.EliminationCondition);
			var actions = entity.Actions.Select(x => ActionViewModel.CreateFromData(x, stringLookup));
			var results = entity.Results.Select(x => ResultViewModel.CreateFromData(x, stringLookup));
			var actionMatrix = ActionMatrixViewModel.CreateFromData(entity.ActionMatrix.Values);
			return new EntityRulesViewModel(entity.Name, preBattleEffects, postBattleEffects, preTurnEffects, postTurnEffects, eliminationCondition, actions, results, actionMatrix, stringLookup);
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
				SetPropertyField(new [] {nameof(Name), nameof(RenderedName) }, value, ref m_name);
			}
		}

		public string RenderedName => StringLookup.Lookup(m_name);

		public ConditionViewModelBase EliminationCondition
		{
			get
			{
				VerifyAccess();
				return m_eliminationCondition;
			}
			set
			{
				SetPropertyField(value, ref m_eliminationCondition);
			}
		}

		public StringLookupViewModel StringLookup { get; }

		public ActionMatrixViewModel ActionMatrix { get; }

		public LabeledViewModelBase SelectedRule
		{
			get
			{
				VerifyAccess();
				return m_selectedRule;
			}
			set
			{
				if (value is TreeNodeViewModelBase)
					SetPropertyField(null, ref m_selectedRule);
				else
					SetPropertyField(value, ref m_selectedRule);
			}
		}

		public IReadOnlyList<TreeNodeViewModelBase> Rules { get; }

		public EntityRules ToEntityRules()
		{
			var entity = new EntityRules(m_name, StringLookup.ToStringLookup());
			entity.PreBattleEffects = m_preBattleEffectsNode.GetChildren().Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			entity.PostBattleEffects = m_postBattleEffectsNode.GetChildren().Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			entity.PreTurnEffects = m_preTurnEffectsNode.GetChildren().Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			entity.PostTurnEffects = m_postTurnEffectsNode.GetChildren().Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			entity.EliminationCondition = m_eliminationCondition.ToConditionBase();
			entity.Actions = m_actionsNode.GetChildren().Select(x => x.ToAction()).ToList().AsReadOnly();
			entity.Results = m_resultsNode.GetChildren().Select(x => x.ToResult()).ToList().AsReadOnly();
			entity.SetActionMatrix(ActionMatrix.ToActionMatrixEntries());
			return entity;
		}

		private EntityRulesViewModel(string name, IEnumerable<EffectViewModelBase> preBattleEffects, IEnumerable<EffectViewModelBase> postBattleEffects, IEnumerable<EffectViewModelBase> preTurnEffects, IEnumerable<EffectViewModelBase> postTurnEffects, ConditionViewModelBase eliminationCondition, IEnumerable<ActionViewModel> actions, IEnumerable<ResultViewModel> results, ActionMatrixViewModel actionMatrix, StringLookupViewModel stringLookup)
		{
			m_name = name;
			m_eliminationCondition = eliminationCondition;

			m_selectedRule = new TreeNodeViewModel<LabeledViewModelBase>("", Enumerable.Empty<LabeledViewModelBase>());

			m_preBattleEffectsNode = new TreeNodeViewModel<EffectViewModelBase>(OurResources.PreBattleEffectsNodeLabel, preBattleEffects);
			m_postBattleEffectsNode = new TreeNodeViewModel<EffectViewModelBase>(OurResources.PostBattleEffectsNodeLabel, postBattleEffects);
			m_preTurnEffectsNode = new TreeNodeViewModel<EffectViewModelBase>(OurResources.PreTurnEffectsNodeLabel, preTurnEffects);
			m_postTurnEffectsNode = new TreeNodeViewModel<EffectViewModelBase>(OurResources.PostTurnEffectsNodeLabel, postTurnEffects);
			m_actionsNode = new TreeNodeViewModel<ActionViewModel>(OurResources.ActionsNodeLabel, actions);
			m_resultsNode = new TreeNodeViewModel<ResultViewModel>(OurResources.ResultsNodeLabel, results);

			Rules = new List<TreeNodeViewModelBase>
			{
				m_preBattleEffectsNode,
				m_preTurnEffectsNode,
				m_postTurnEffectsNode,
				m_postBattleEffectsNode,
				m_actionsNode,
				m_resultsNode,
			};

			ActionMatrix = actionMatrix;
			StringLookup = stringLookup;
		}

		readonly TreeNodeViewModel<EffectViewModelBase> m_preBattleEffectsNode;
		readonly TreeNodeViewModel<EffectViewModelBase> m_postBattleEffectsNode;
		readonly TreeNodeViewModel<EffectViewModelBase> m_preTurnEffectsNode;
		readonly TreeNodeViewModel<EffectViewModelBase> m_postTurnEffectsNode;
		readonly TreeNodeViewModel<ActionViewModel> m_actionsNode;
		readonly TreeNodeViewModel<ResultViewModel> m_resultsNode;

		string m_name;
		ConditionViewModelBase m_eliminationCondition;
		LabeledViewModelBase m_selectedRule;
	}
}
