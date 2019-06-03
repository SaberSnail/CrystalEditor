using CrystalDuelingEngine.Rules;
using CrystalEditor.Serialization;

namespace CrystalEditor.ViewModels
{
	public sealed class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			LoadFiles();
		}

		public GameRulesViewModel GameRules
		{
			get
			{
				VerifyAccess();
				return m_gameRules;
			}
			private set
			{
				SetPropertyField(value, ref m_gameRules);
			}
		}

		public EntityRulesViewModel EntityRules
		{
			get
			{
				VerifyAccess();
				return m_entityRules;
			}
			private set
			{
				SetPropertyField(value, ref m_entityRules);
			}
		}

		private void LoadFiles()
		{
			GameRules = LoadGameRules(@"c:\Temp\Rules.txt");
			EntityRules = LoadEntityRules(@"c:\Temp\ManInChainmailEntity.txt");
		}

		private GameRulesViewModel LoadGameRules(string filename)
		{
			string json;
			using (var file = new System.IO.StreamReader(filename))
				json = file.ReadToEnd().Trim();
			var gameRules = JsonDeserializer.Deserialize(json) as GameRules;
			return GameRulesViewModel.CreateFromData(gameRules);
		}

		private EntityRulesViewModel LoadEntityRules(string filename)
		{
			string json;
			using (var file = new System.IO.StreamReader(filename))
				json = file.ReadToEnd().Trim();
			var entityRules = JsonDeserializer.Deserialize(json) as EntityRules;
			return EntityRulesViewModel.CreateFromData(entityRules);
		}

		GameRulesViewModel m_gameRules;
		EntityRulesViewModel m_entityRules;
	}
}
