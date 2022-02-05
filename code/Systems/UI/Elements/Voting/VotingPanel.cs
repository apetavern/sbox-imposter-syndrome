using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.States;
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
		private Panel FooterPanel { get; set; }
		private Dictionary<PlayerPanel, ISPlayer> PlayerPanels { get; set; }


		public VotingPanel()
		{
			StyleSheet.Load( "/Systems/UI/Elements/Voting/VotingPanel.scss" );
			Instance = this;
			PlayerPanels = new();
		}

		public void Show( bool shouldShow, int calledByPlayerNetIdent )
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
				playerPanel.CheckIfMeetingCaller( calledByPlayerNetIdent );

				container.AddChild( playerPanel );
				PlayerPanels.Add( playerPanel, player );
			}

			FooterPanel = Add.Panel( "footer" );
			FooterPanel.Add.Button( "Skip", () => VotingState.ReceiveVote( -1 ) );
		}

		public override void Tick()
		{
			base.Tick();

			if ( ImposterSyndrome.Instance.CurrentState is null || TimerLabel is null )
				return;

			var stateEndTime = ImposterSyndrome.Instance.CurrentState.StateEndTime;
			TimerLabel.Text = MathX.Clamp( (int)(stateEndTime - Time.Now), 0, 500 ).ToString() + "s";
		}

		public void UpdateVoteFromPlayer( int voteFromNetIdent, int voteToNetIdent )
		{
			var voteFromPlayer = Entity.All.FirstOrDefault( ent => ent.NetworkIdent == voteFromNetIdent ) as ISPlayer;

			if ( voteFromPlayer is null )
				return;

			if ( voteToNetIdent < 0 )
			{
				var vote = FooterPanel.Add.Panel( "vote" );
				vote.Style.BackgroundColor = voteFromPlayer.PlayerColor;
				return;
			}

			var votedForPlayer = Entity.All.FirstOrDefault( ent => ent.NetworkIdent == voteToNetIdent ) as ISPlayer;

			if ( votedForPlayer is null )
				return;

			var playerPanel = PlayerPanels.FirstOrDefault( entry => entry.Value == votedForPlayer ).Key;

			playerPanel.UpdateVote( voteFromPlayer );
		}
	}
}
