using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class GameConfigPanel : Panel
	{
		public static GameConfigPanel Instance { get; set; }

		private Panel OptionsPanel { get; set; }

		public SliderEntry NumberOfImposters;
		public SliderEntry NumberOfTasks;

		public GameConfigPanel()
		{
			Instance = this;
			StyleSheet.Load( "/Systems/UI/Menu/Right/Elements/GameConfigPanel.scss" );

			Rebuild();
		}

		public void Rebuild()
		{
			DeleteChildren( true );

			Add.Label( "Game Configuration", "title" );
			Add.Label( "Various settings for gameplay.", "subtitle" );

			AddOption( ref NumberOfImposters, "Number of Imposters", 1, 4, 2 );
			AddOption( ref NumberOfTasks, "Number of Tasks", 4, 6, 4 );
		}

		private void AddOption( ref SliderEntry entry, string title, int minValue, int maxValue, int defaultValue )
		{
			var formRow = Add.Panel( "form-row" );

			formRow.Add.Label( title, "text" );

			var formValue = formRow.Add.Panel( "form-value" );

			entry = formValue.AddChild<SliderEntry>();
			entry.Value = defaultValue;
			entry.MinValue = minValue;
			entry.MaxValue = maxValue;
		}
	}
}
