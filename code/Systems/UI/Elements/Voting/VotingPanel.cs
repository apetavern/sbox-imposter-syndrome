using ImposterSyndrome.Systems.Players;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.UI
{
	public class VotingPanel : Panel
	{
		public static VotingPanel Instance { get; set; }
		private Label TimerLabel { get; set; }
		private Dictionary<PlayerPanel, ISPlayer> PlayerPanels { get; set; }

		public VotingPanel()
		{
			StyleSheet.Load( "/Systems/UI/Elements/Voting/VotingPanel.scss" );
			Instance = this;
			PlayerPanels = new();
		}

		public void Show( bool shouldShow )
		{
			DeleteChildren( true );
			PlayerPanels.Clear();

			SetClass( "visible", shouldShow );

			if ( !shouldShow )
				return;

			var header = Add.Panel( "header" );
			header.Add.Label( "Who is the imposter?", "Title" );
			TimerLabel = header.Add.Label( "0s", "Timer" );

			var container = Add.Panel( "container" );

			foreach ( var player in ImposterSyndrome.Instance?.Players )
			{
				var playerPanel = new PlayerPanel( player );

				container.AddChild( playerPanel );
				PlayerPanels.Add( playerPanel, player );
			}

			var footer = Add.Panel( "footer" );
			footer.Add.Button( "Skip", () => Log.Info( "SKIP" ) );
		}

		public override void Tick()
		{
			base.Tick();

			if ( ImposterSyndrome.Instance.CurrentState is null || TimerLabel is null )
				return;

			var stateEndTime = ImposterSyndrome.Instance.CurrentState.StateEndTime;
			TimerLabel.Text = MathX.Clamp( (int)(stateEndTime - Time.Now), 0, 500 ).ToString() + "s";
		}

		public void UpdatePlayerPanel( int voteFromNetIdent, int voteToNetIdent )
		{
			var votedForPlayer = Entity.All.FirstOrDefault( ent => ent.NetworkIdent == voteToNetIdent ) as ISPlayer;

			if ( votedForPlayer is null )
				return;

			var voteFromPlayer = Entity.All.FirstOrDefault( ent => ent.NetworkIdent == voteFromNetIdent ) as ISPlayer;

			if ( voteFromPlayer is null )
				return;

			var playerPanel = PlayerPanels.FirstOrDefault( entry => entry.Value == votedForPlayer ).Key;

			playerPanel.UpdateVote( voteFromPlayer );
		}
	}
}
