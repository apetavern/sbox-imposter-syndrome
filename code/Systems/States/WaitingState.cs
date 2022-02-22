using ImposterSyndrome.Systems.Entities;
using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.States
{
	public partial class WaitingState : BaseState
	{
		[Net] public override string StateName => "Waiting";
		public override float StateDuration { get; set; } = 5;
		[Net] public bool HasPrematchStarted { get; set; }

		public override void OnStateStarted()
		{
			foreach ( var player in Client.All.Select( x => x.Pawn as ISBasePlayer ) )
			{
				player.UpdatePawn( new ISSpectator() );
			}

			PlayingHudEntity.Rebuild();
			DoPostGameCleanup();
		}

		public override void OnStateEnded()
		{
			// Ensure everybody has a color before starting the game.
			ImposterSyndrome.AssignRemainingColors();

			ImposterSyndrome.UpdateState( new SetupState() );
		}

		public override void OnSecond()
		{
			if ( Host.IsClient )
				return;

			if ( !HasPrematchStarted )
				return;

			if ( Time.Now > StateEndTime )
				OnStateEnded();
		}

		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new ISSpectator();
			player.Respawn();
			client.Pawn = player;

			PlayingHudEntity.RefreshConfigPanel();
		}

		public override void ClientDisconnect( Client client )
		{
			base.ClientDisconnect( client );

			PlayingHudEntity.RefreshConfigPanel();
		}

		private void DoPostGameCleanup()
		{
			// Clear player list.
			ImposterSyndrome.Instance.Players.Clear();

			// Make all player colors available again.
			ImposterSyndrome.ResetColorAssignment();

			// Cleanup bodies.
			DeadPlayerEntity.RemoveAll();

			// Reset all world entities.
			BaseUsableEntity.ResetAll();
		}

		[ServerCmd]
		public static void Startup( bool shouldStartup )
		{
			if ( Host.IsClient )
				return;

			if ( Client.All.Count < GameConfig.MinimumPlayers )
				return;

			if ( ImposterSyndrome.Instance.CurrentState is not WaitingState waitingState )
				return;

			waitingState.HasPrematchStarted = shouldStartup;

			if ( shouldStartup )
				waitingState.StateEndTime = Time.Now + waitingState.StateDuration;
		}
	}
}
