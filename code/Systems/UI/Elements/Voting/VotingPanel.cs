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

			SetClass( "visible", shouldShow );

			if ( !shouldShow )
				return;

			Add.Label( "Who is the imposter?", "Title" );

			var container = Add.Panel( "container" );
			foreach ( var player in ImposterSyndrome.Instance.Players )
			{
				container.AddChild( new PlayerPanel( player ) );
			}
		}
	}
}
