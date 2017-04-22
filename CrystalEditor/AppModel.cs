using System;
using System.Windows;
using CrystalDuelingEngine.Rules;
using CrystalEditor.MainWindow;
using CrystalEditor.Serialization;
using CrystalEditor.ViewModels;
using GoldenAnvil.Utility;
using GoldenAnvil.Utility.Logging;

namespace CrystalEditor
{
	public sealed class AppModel
	{
		public static string OrganizationName
		{
			get { return "AStott Productions"; }
		}

		public static string ApplicationName
		{
			get { return "Crystal Editor"; }
		}

		public static AppModel Current
		{
			get
			{
				return ((App) Application.Current).AppModel;
			}
		}

		public AppModel()
		{
		}

		public event EventHandler OnStartupFinished;

		public MainWindowViewModel MainWindowViewModel { get; set; }

		public void Startup()
		{
			LogManager.Initialize(new ConsoleLogDestination(false));

			RegisterFactories();

			RunTemporaryTests();

			MainWindowViewModel = new MainWindowViewModel();

			OnStartupFinished.Raise(this);
		}

		public void Shutdown()
		{
		}

		private void RegisterFactories()
		{
			AddTagEffectViewModel.RegisterFactory();
			CopyTagEffectViewModel.RegisterFactory();
			RemoveTagsEffectViewModel.RegisterFactory();

			ConditionTagViewModel.RegisterFactory();
			ConditionalTagsTagViewModel.RegisterFactory();
			SimpleTagViewModel.RegisterFactory();
			IntValueTagViewModel.RegisterFactory();
			StringValueTagViewModel.RegisterFactory();

			AlwaysConditionViewModel.RegisterFactory();
			BinaryLogicConditionViewModel.RegisterFactory();
			UnaryLogicConditionViewModel.RegisterFactory();
			HasTagConditionViewModel.RegisterFactory();
			ValueCompareConditionViewModel.RegisterFactory();
		}

		private void RunTemporaryTests()
		{
			string json;
			string newJson;

			// test game rules
			using (Log.TimedInfo("GameRules serialization and view model test"))
			{
				using (var file = new System.IO.StreamReader("c:\\Temp\\Rules.txt"))
					json = file.ReadToEnd().Trim();
				var gameRules = JsonDeserializer.Deserialize(json) as GameRules;
				newJson = JsonSerializer.Serialize(gameRules);
				if (newJson != json)
					throw new InvalidOperationException("Serialization round trip failed for GameRules.");

				var rulesViewModel = GameRulesViewModel.CreateFromData(gameRules);
				var newGameRules = rulesViewModel.ToGameRules();
				newJson = JsonSerializer.Serialize(newGameRules);
				if (newJson != json)
					throw new InvalidOperationException("ViewModel round trip failed for GameRules.");
			}

			// test entity rules
			using (Log.TimedInfo("Giant Goblin EntityRules serialization and view model test"))
			{
				using (var file = new System.IO.StreamReader("c:\\Temp\\GiantGoblinEntity.txt"))
					json = file.ReadToEnd().Trim();
				var goblin = JsonDeserializer.Deserialize(json) as EntityRules;
				newJson = JsonSerializer.Serialize(goblin);
				if (newJson != json)
					throw new InvalidOperationException("Serialization round trip failed for EntityRules.");

				var goblinViewModel = EntityRulesViewModel.CreateFromData(goblin);
				var newGoblinRules = goblinViewModel.ToEntityRules();
				newJson = JsonSerializer.Serialize(newGoblinRules);
				if (newJson != json)
					throw new InvalidOperationException("ViewModel round trip failed for goblin EntityRules.");
			}

			// test entity rules
			using (Log.TimedInfo("Man in Chainmail EntityRules serialization and view model test"))
			{
				using (var file = new System.IO.StreamReader("c:\\Temp\\ManInChainmailEntity.txt"))
					json = file.ReadToEnd().Trim();
				var chainmail = JsonDeserializer.Deserialize(json) as EntityRules;
				newJson = JsonSerializer.Serialize(chainmail);
				if (newJson != json)
					throw new InvalidOperationException("Serialization round trip failed for EntityRules.");

				var chainmailViewModel = EntityRulesViewModel.CreateFromData(chainmail);
				var newChainmailRules = chainmailViewModel.ToEntityRules();
				newJson = JsonSerializer.Serialize(newChainmailRules);
				if (newJson != json)
					throw new InvalidOperationException("ViewModel round trip failed for chainmail EntityRules.");
			}
		}

		static readonly ILogSource Log = LogManager.CreateLogSource(nameof(AppModel));
	}
}
