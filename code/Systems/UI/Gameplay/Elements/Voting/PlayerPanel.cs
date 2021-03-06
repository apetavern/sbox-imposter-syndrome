using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.States;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class PlayerPanel : Panel
	{
		private ISPlayer HeldPlayer { get; set; }
		private Label MeetingCallerStatus { get; set; }
		private Panel VotePanel { get; set; }

		public PlayerPanel( ISPlayer player )
		{
			StyleSheet.Load( "/Systems/UI/Gameplay/Elements/Voting/PlayerPanel.scss" );

			var colorPanel = Add.Panel( "colour" );
			colorPanel.Style.BackgroundColor = player.PlayerColor;
			colorPanel.SetClass( "dead", player.LifeState != LifeState.Alive );

			var playerInfo = Add.Panel( "info" );
			playerInfo.Add.Label( player.Client.Name, "name" );
			VotePanel = playerInfo.Add.Panel( "votes" );
			MeetingCallerStatus = playerInfo.Add.Label( "!", "calledmeeting" );

			HeldPlayer = player;
			SetClass( "dead", player.LifeState != LifeState.Alive );

			AddEventListener( "onclick", () => Click( HeldPlayer ) );
		}

		public void CheckIfMeetingCaller( int calledByPlayerNetIdent )
		{
			if ( HeldPlayer.NetworkIdent == calledByPlayerNetIdent )
				MeetingCallerStatus.SetClass( "visible", true );
		}

		private void Click( ISPlayer votedForPlayer )
		{
			if ( HeldPlayer.LifeState != LifeState.Alive )
				return;

			VotingState.ReceiveVote( votedForPlayer.NetworkIdent );
		}

		public void UpdateVote( ISPlayer voteFromPlayer )
		{
			var vote = VotePanel.Add.Panel( "vote" );
			vote.Style.BackgroundColor = voteFromPlayer.PlayerColor;
		}
	}
}
