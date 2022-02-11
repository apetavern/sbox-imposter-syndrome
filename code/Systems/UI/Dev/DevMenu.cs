using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI.Dev
{
	public class DevMenu : Panel
	{
		public DevMenu()
		{
			StyleSheet.Load( "/Systems/UI/Dev/DevMenu.scss" );
			BindClass( "visible", () => Input.Down( InputButton.Flashlight ) );

			ConsoleSystem.Run( "sv_cheats 1" );

			// General panel
			Add.Label( "General", "title" );

			var genButtons = Add.Panel( "buttons" );
			genButtons.Add.ButtonWithIcon( "Add a bot", "smart_toy", "button", () => ConsoleSystem.Run( "bot_add 0 0" ) );
			genButtons.Add.ButtonWithIcon( "Suicide", "clear", "button", () => ConsoleSystem.Run( "kill" ) );
			genButtons.Add.ButtonWithIcon( "Reload HUD", "refresh", "button", () => ConsoleSystem.Run( "rebuild" ) );

			// States panel
			Add.Label( "States", "title" );

			var stateButtons = Add.Panel( "buttons" );
			stateButtons.Add.ButtonWithIcon( "Startup", "schedule", "button", () => ConsoleSystem.Run( "updatestate waiting" ) );
			stateButtons.Add.ButtonWithIcon( "End Game", "close", "button", () => ConsoleSystem.Run( "updatestate end" ) );

			// Players panel

			Add.Label( "Players", "title" );

			var row = Add.Panel();
			var dropdown = new DropDown( row );
			AddEventListener( "onopen", () =>
			{
				dropdown.Options.Clear();

				foreach ( var player in Client.All )
				{
					dropdown.Options.Add( new Option( player.Name, player.Name ) );
				}
			} );

			row.Add.ButtonWithIcon( "Kill", "close", "button", () => ConsoleSystem.Run( $"killtarget {dropdown.Value}" ) );

			var playerButtons = Add.Panel( "buttons" );
			playerButtons.Add.ButtonWithIcon( "Force Imposter", "close", "button", () => ConsoleSystem.Run( $"forceimposter true" ) );
			playerButtons.Add.ButtonWithIcon( "Force Player", "close", "button", () => ConsoleSystem.Run( $"forceimposter false" ) );
		}

		public override void Tick()
		{
			base.Tick();

			if ( Input.Pressed( InputButton.Flashlight ) )
			{
				CreateEvent( "onopen" );
			}
		}
	}
}
