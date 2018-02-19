using CrystalDuelingEngine.Rules;
using CrystalEditor.GameRulesView;
using CrystalEditor.Serialization;
using CrystalEditor.ViewModels;

namespace CrystalEditor.MainWindow
{
	public sealed class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			LoadFile();
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
				SetPropertyField(nameof(GameRules), value, ref m_gameRules);
			}
		}

		private void LoadFile()
		{
			string json;
			using (var file = new System.IO.StreamReader("c:\\Temp\\Rules.txt"))
				json = file.ReadToEnd().Trim();
			var gameRules = JsonDeserializer.Deserialize(json) as GameRules;
			GameRules = GameRulesViewModel.CreateFromData(gameRules);
		}

		GameRulesViewModel m_gameRules;
	}
}
