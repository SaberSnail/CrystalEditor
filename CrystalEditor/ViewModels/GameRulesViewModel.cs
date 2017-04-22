using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CrystalDuelingEngine.Rules;

namespace CrystalEditor.ViewModels
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

		public ObservableCollection<EffectViewModelBase> PreBattleEffects { get; }

		public ObservableCollection<EffectViewModelBase> PostBattleEffects { get; }

		public ObservableCollection<EffectViewModelBase> PostTurnEffects { get; }

		public ObservableCollection<EffectViewModelBase> PreResultEffects { get; }

		public ObservableCollection<EffectViewModelBase> PostResultEffects { get; }

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
			var rules = new GameRules();
			rules.PreBattleEffects = PreBattleEffects.Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			rules.PostBattleEffects = PostBattleEffects.Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			rules.PostTurnEffects = PostTurnEffects.Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			rules.PreResultEffects = PreResultEffects.Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			rules.PostResultEffects = PostResultEffects.Select(x => x.ToEffectBase()).ToList().AsReadOnly();
			rules.EliminationCondition = m_eliminationCondition.ToConditionBase();
			return rules;
		}

		private GameRulesViewModel(IEnumerable<EffectViewModelBase> preBattleEffects, IEnumerable<EffectViewModelBase> postBattleEffects, IEnumerable<EffectViewModelBase> postTurnEffects, IEnumerable<EffectViewModelBase> preResultEffects, IEnumerable<EffectViewModelBase> postResultEffects, ConditionViewModelBase eliminationCondition)
		{
			PreBattleEffects = new ObservableCollection<EffectViewModelBase>(preBattleEffects);
			PostBattleEffects = new ObservableCollection<EffectViewModelBase>(postBattleEffects);
			PostTurnEffects = new ObservableCollection<EffectViewModelBase>(postTurnEffects);
			PreResultEffects = new ObservableCollection<EffectViewModelBase>(preResultEffects);
			PostResultEffects = new ObservableCollection<EffectViewModelBase>(postResultEffects);
			m_eliminationCondition = eliminationCondition;
		}

		ConditionViewModelBase m_eliminationCondition;
	}
}
