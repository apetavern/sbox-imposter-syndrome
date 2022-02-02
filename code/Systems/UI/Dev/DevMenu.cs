using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class DevMenu : Panel
	{
		public DevMenu()
		{
			StyleSheet.Load( "/Systems/UI/Dev/DevMenu.scss" );
			BindClass( "visible", () => Input.Down( InputButton.Flashlight ) );

			Add.Label( "General", "title" );
			var genButtons = Add.Panel( "buttons" );
			genButtons.Add.ButtonWithIcon( "Add a bot", "smart_toy", "button", () => ConsoleSystem.Run( "bot_add 0 0" ) );
			genButtons.Add.ButtonWithIcon( "Suicide", "clear", "button", () => ConsoleSystem.Run( "kill" ) );
			genButtons.Add.ButtonWithIcon( "Reload HUD", "refresh", "button", () => ConsoleSystem.Run( "rebuild" ) );

			Add.Label( "States", "title" );
			var stateButtons = Add.Panel( "buttons" );
			stateButtons.Add.ButtonWithIcon( "Waiting", "schedule", "button", () => ConsoleSystem.Run( "updatestate waiting" ) );
			stateButtons.Add.ButtonWithIcon( "Playing", "videogame_asset", "button", () => ConsoleSystem.Run( "updatestate playing" ) );
			stateButtons.Add.ButtonWithIcon( "End Game", "close", "button", () => ConsoleSystem.Run( "updatestate end" ) );
		}
	}
}
