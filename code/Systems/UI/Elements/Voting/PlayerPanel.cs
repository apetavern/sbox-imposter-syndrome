using ImposterSyndrome.Systems.Players;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class PlayerPanel : Panel
	{
		private ISPlayer HeldPlayer { get; set; }

		public PlayerPanel( ISPlayer player )
		{
			StyleSheet.Load( "/Systems/UI/Elements/Voting/PlayerPanel.scss" );

			Add.Panel( "colour" );

			var playerInfo = Add.Panel( "info" );
			playerInfo.Add.Label( player.Client.Name, "name" );
			playerInfo.Add.Panel( "votes" );

			HeldPlayer = player;

			AddEventListener( "onclick", () => Click() );
		}

		private void Click()
		{
			Log.Info( $"{HeldPlayer.Client.Name} clicked." );
		}
	}
}
