using ImposterSyndrome.Systems.Entities;
using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ImposterSyndrome.Systems.States
{
	public partial class VotingState : BaseState
	{
		[Net] public override string StateName => "Voting";

		// Voting player, voted for player
		[Net] public Dictionary<ISPlayer, ISPlayer> PlayerVotes { get; set; }
		public override float StateDuration { get; set; } = 30;
		public int CalledByPlayerNetIdent { get; set; } = -1;

		public VotingState() { }

		public VotingState( ISPlayer calledByPlayer ) : this()
		{
			CalledByPlayerNetIdent = calledByPlayer.NetworkIdent;
		}

		public override void OnStateStarted()
		{
			base.OnStateStarted();

			// Send these over to the players HUDs.
			PlayerHudEntity.ShowVotingScreen( true, CalledByPlayerNetIdent >= 0 ? CalledByPlayerNetIdent : -1 );

			ISBasePlayer.ReturnAllToCampfire();
		}

		public override void OnStateEnded()
		{
			PlayerHudEntity.ShowVotingScreen( false );

			// Delete these dead players.
			DeadPlayerEntity.RemoveAll();

			ImposterSyndrome.UpdateState( new PlayingState() );
		}

		[ServerCmd]
		public static void ReceiveVote( int voteToPlayerNetId )
		{
			Host.AssertServer();

			if ( !Host.IsServer )
				return;

			if ( ImposterSyndrome.Instance.CurrentState is not VotingState votingState )
				return;

			if ( ConsoleSystem.Caller.Pawn is not ISPlayer votingFromPlayer || votingFromPlayer.LifeState != LifeState.Alive )
				return;

			if ( votingState.PlayerVotes.ContainsKey( votingFromPlayer ) )
			{
				Log.Info( "already voted" );
				return;
			}

			// Skipping
			if ( voteToPlayerNetId < 0 )
			{
				votingState.PlayerVotes.Add( votingFromPlayer, null );

				PlayerHudEntity.ReceivePlayerVote( -1, votingFromPlayer.NetworkIdent );
				return;
			}

			var votedForPlayer = Entity.All.FirstOrDefault( ent => ent.NetworkIdent == voteToPlayerNetId ) as ISPlayer;

			if ( votedForPlayer is null || votedForPlayer.LifeState != LifeState.Alive )
				return;

			votingState.PlayerVotes.Add( votingFromPlayer, votedForPlayer );
			PlayerHudEntity.ReceivePlayerVote( voteToPlayerNetId, votingFromPlayer.NetworkIdent );
		}
	}
}
