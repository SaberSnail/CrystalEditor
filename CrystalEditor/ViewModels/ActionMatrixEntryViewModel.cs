using CrystalDuelingEngine;

namespace CrystalEditor.ViewModels
{
	public sealed class ActionMatrixEntryViewModel : ViewModelBase
	{
		public static ActionMatrixEntryViewModel CreateFromData(ActionMatrixEntry entry)
		{
			return new ActionMatrixEntryViewModel(entry.AttackerActionId, entry.DefenderActionId, entry.ResultId);
		}

		public string AttackerActionId
		{
			get
			{
				VerifyAccess();
				return m_attackerActionId;
			}
			set
			{
				SetPropertyField(nameof(AttackerActionId), value, ref m_attackerActionId);
			}
		}

		public string DefenderActionId
		{
			get
			{
				VerifyAccess();
				return m_defenderActionId;
			}
			set
			{
				SetPropertyField(nameof(DefenderActionId), value, ref m_defenderActionId);
			}
		}

		public string ResultId
		{
			get
			{
				VerifyAccess();
				return m_resultId;
			}
			set
			{
				SetPropertyField(nameof(ResultId), value, ref m_resultId);
			}
		}

		public ActionMatrixEntry ToActionMatrixEntry()
		{
			return new ActionMatrixEntry(m_attackerActionId, m_defenderActionId, m_resultId);
		}

		private ActionMatrixEntryViewModel(string attackerActionId, string defenderActionId, string resultId)
		{
			AttackerActionId = attackerActionId;
			DefenderActionId = defenderActionId;
			ResultId = resultId;
		}

		string m_attackerActionId;
		string m_defenderActionId;
		string m_resultId;
	}
}
