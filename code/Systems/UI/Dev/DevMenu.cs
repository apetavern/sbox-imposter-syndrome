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
			var buttons = Add.Panel( "buttons" );

			buttons.Add.ButtonWithIcon( "Add a bot", "smart_toy", "button", () =>
			{
				ConsoleSystem.Run( "bot_add 0 0" );
			} );

			buttons.Add.ButtonWithIcon( "Suicide", "clear", "button", () =>
			{
				ConsoleSystem.Run( "kill" );
			} );

			buttons.Add.ButtonWithIcon( "Reload HUD", "refresh", "button", () =>
			{
				ConsoleSystem.Run( "rebuild" );
			} );

			Add.Label( "States", "title" );

			var buttons1 = Add.Panel( "buttons" );

			buttons1.Add.ButtonWithIcon( "Waiting", "schedule", "button", () =>
			{
				ConsoleSystem.Run( "updatestate waiting" );
			} );

			buttons1.Add.ButtonWithIcon( "Playing", "videogame_asset", "button", () =>
			{
				ConsoleSystem.Run( "updatestate playing" );
			} );

			buttons1.Add.ButtonWithIcon( "End Game", "close", "button", () =>
			{
				ConsoleSystem.Run( "updatestate end" );
			} );
		}
	}
}
