using ImposterSyndrome.Systems.States;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class MenuLeftPanel : Panel
	{
		private Button PlayButton { get; set; }
		private bool PlayButtonClicked { get; set; }

		public MenuLeftPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/MenuLeftPanel.scss" );

			Add.Panel( "logo" );

			AddChild<MenuPlayerListPanel>();

			PlayButton = Add.Button( "START", "playbutton", () => OnPlayClicked() );
			PlayButton.BindClass( "disabled", () => Client.All.Count < GameConfig.MinimumPlayers );
			PlayButton.BindClass( "clicked", () => PlayButtonClicked );
		}

		private void OnPlayClicked()
		{
			if ( Client.All.Count < GameConfig.MinimumPlayers )
				return;

			PlayButtonClicked = !PlayButtonClicked;

			if ( PlayButtonClicked )
				PlayButton.Text = "CANCEL";
			else
				PlayButton.Text = "START";

			WaitingState.Startup( PlayButtonClicked );

			if ( GameConfigPanel.Instance is null )
				return;

			GameConfig.ReceiveMenuConfig( GameConfigPanel.Instance.PlayersPerImposter, GameConfigPanel.Instance.NumberOfTasks );
		}
	}
}
