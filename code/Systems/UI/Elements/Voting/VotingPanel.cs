using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class VotingPanel : Panel
	{
		public static VotingPanel Instance { get; set; }

		public VotingPanel()
		{
			StyleSheet.Load( "/Systems/UI/Elements/Voting/VotingPanel.scss" );
			Instance = this;
		}

		public void Show( bool shouldShow )
		{
			DeleteChildren( true );

			Log.Info( shouldShow );

			if ( !shouldShow )
				return;

			Add.Label( "Who is the imposter?", "Title" );

			var container = Add.Panel( "container" );

			foreach ( var player in ImposterSyndrome.Instance.Players )
			{
				var playerContainer = container.Add.Panel( "player" );
				playerContainer.Add.Panel( "colour" );

				var playerInfo = playerContainer.Add.Panel( "info" );
				playerInfo.Add.Label( "Name", "name" );
				playerInfo.Add.Panel( "votes" );
			}
		}
	}
}
