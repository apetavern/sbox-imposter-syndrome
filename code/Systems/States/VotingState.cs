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
		public override float StateDuration { get; set; } = 10;
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
			PlayingHudEntity.ShowVotingScreen( true, CalledByPlayerNetIdent >= 0 ? CalledByPlayerNetIdent : -1 );

			ISBasePlayer.ReturnAllToCampfire();
		}

		public override void OnStateEnded()
		{
			PlayingHudEntity.ShowVotingScreen( false );

			EjectMajorityVotedPlayer();

			// Delete these dead players.
			DeadPlayerEntity.RemoveAll();

			ImposterSyndrome.UpdateState( new PlayingState() );
		}

		private void EjectMajorityVotedPlayer()
		{
			// Calculate which player to eject.
			var sortedDict = PlayerVotes.Where( x => x.Value is not null ).GroupBy( x => x.Value ).ToDictionary( x => x.Key, x => x.Count() ).OrderByDescending( x => x ).FirstOrDefault();

			var highestVotedPlayer = sortedDict.Key;
			var numberOfVotesForHighestVoted = sortedDict.Value;

			var totalAlivePlayers = ImposterSyndrome.Instance?.Players.Count( player => player.LifeState == LifeState.Alive );

			if ( numberOfVotesForHighestVoted >= totalAlivePlayers / 2 )
				highestVotedPlayer?.Eject();
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
				return;

			// Skipping
			if ( voteToPlayerNetId < 0 )
			{
				votingState.PlayerVotes.Add( votingFromPlayer, null );

				PlayingHudEntity.ReceivePlayerVote( -1, votingFromPlayer.NetworkIdent );
				return;
			}

			var votedForPlayer = Entity.All.FirstOrDefault( ent => ent.NetworkIdent == voteToPlayerNetId ) as ISPlayer;

			if ( votedForPlayer is null || votedForPlayer.LifeState != LifeState.Alive )
				return;

			votingState.PlayerVotes.Add( votingFromPlayer, votedForPlayer );

			PlayingHudEntity.ReceivePlayerVote( voteToPlayerNetId, votingFromPlayer.NetworkIdent );
		}
	}
}
