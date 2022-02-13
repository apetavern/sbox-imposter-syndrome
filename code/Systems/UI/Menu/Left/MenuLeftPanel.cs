using ImposterSyndrome.Systems.States;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class MenuLeftPanel : Panel
	{
		public MenuLeftPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/MenuLeftPanel.scss" );

			Add.Panel( "logo" );

			AddChild<MenuPlayerListPanel>();

			var playButton = Add.Button( "PLAY", "playbutton", () => WaitingState.Startup() );
			playButton.BindClass( "disabled", () => Client.All.Count < GameConfig.MinimumPlayers );
		}
	}
}
