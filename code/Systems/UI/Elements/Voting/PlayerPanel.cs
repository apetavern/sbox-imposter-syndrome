using ImposterSyndrome.Systems.Players;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class PlayerPanel : Panel
	{
		public PlayerPanel( ISPlayer player )
		{
			StyleSheet.Load( "/Systems/UI/Elements/Voting/PlayerPanel.scss" );

			Add.Panel( "colour" );

			var playerInfo = Add.Panel( "info" );
			playerInfo.Add.Label( player.Client.Name, "name" );
			playerInfo.Add.Panel( "votes" );
		}
	}
}
