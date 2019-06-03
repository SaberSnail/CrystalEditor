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
			var preBattleEffects = entity.PreBattleEffects.Select(EffectViewModelBase.CreateFromData);
			var postBattleEffects = entity.PostBattleEffects.Select(EffectViewModelBase.CreateFromData);
			var preTurnEffects = entity.PreTurnEffects.Select(EffectViewModelBase.CreateFromData);
			var postTurnEffects = entity.PostTurnEffects.Select(EffectViewModelBase.CreateFromData);
			var eliminationCondition = ConditionViewModelBase.CreateFromData(entity.EliminationCondition);
			var actions = entity.Actions.Select(ActionViewModel.CreateFromData);
			var results = entity.Results.Select(ResultViewModel.CreateFromData);
			var actionMatrix = ActionMatrixViewModel.CreateFromData(entity.ActionMatrix.Values);
			var stringLookup = StringLookupViewModel.CreateFromData(entity.StringLookup);
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

		public ObservableCollection<EffectViewModelBase> PreBattleEffects { get; }

		public ObservableCollection<EffectViewModelBase> PostBattleEffects { get; }

		public ObservableCollection<EffectViewModelBase> PreTurnEffects { get; }

		public ObservableCollection<EffectViewModelBase> PostTurnEffects { get; }

		public ObservableCollection<ActionViewModel> Actions { get; }

		public ObservableCollection<ResultViewModel> Results { get; }

		public EntityRules ToEntityRules()
		{
			var entity = new EntityRules(m_name, StringLookup.ToStringLookup());
			entity.PreBattleEffects = PreBattleEffects.Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			entity.PostBattleEffects = PostBattleEffects.Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			entity.PreTurnEffects = PreTurnEffects.Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			entity.PostTurnEffects = PostTurnEffects.Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			entity.EliminationCondition = m_eliminationCondition.ToConditionBase();
			entity.Actions = Actions.Select(x => x.ToAction()).ToList().AsReadOnly();
			entity.Results = Results.Select(x => x.ToResult()).ToList().AsReadOnly();
			entity.SetActionMatrix(ActionMatrix.ToActionMatrixEntries());
			return entity;
		}

		private EntityRulesViewModel(string name, IEnumerable<EffectViewModelBase> preBattleEffects, IEnumerable<EffectViewModelBase> postBattleEffects, IEnumerable<EffectViewModelBase> preTurnEffects, IEnumerable<EffectViewModelBase> postTurnEffects, ConditionViewModelBase eliminationCondition, IEnumerable<ActionViewModel> actions, IEnumerable<ResultViewModel> results, ActionMatrixViewModel actionMatrix, StringLookupViewModel stringLookup)
		{
			m_name = name;
			PreBattleEffects = new ObservableCollection<EffectViewModelBase>(preBattleEffects);
			PostBattleEffects = new ObservableCollection<EffectViewModelBase>(postBattleEffects);
			PreTurnEffects = new ObservableCollection<EffectViewModelBase>(preTurnEffects);
			PostTurnEffects = new ObservableCollection<EffectViewModelBase>(postTurnEffects);
			m_eliminationCondition = eliminationCondition;
			Actions = new ObservableCollection<ActionViewModel>(actions);
			Results = new ObservableCollection<ResultViewModel>(results);
			ActionMatrix = actionMatrix;
			StringLookup = stringLookup;
		}

		string m_name;
		ConditionViewModelBase m_eliminationCondition;
	}
}
