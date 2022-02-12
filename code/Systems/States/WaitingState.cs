﻿using ImposterSyndrome.Systems.Entities;
using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.UI;
using Sandbox;
using Sandbox.UI;
using System.Linq;

namespace ImposterSyndrome.Systems.States
{
	public partial class WaitingState : BaseState
	{
		[Net] public override string StateName => "Waiting";
		public override float StateDuration { get; set; } = 5;
		private bool HasPrematchStarted { get; set; }

		public override void OnStateStarted()
		{
			foreach ( var player in Client.All.Select( cl => cl.Pawn as ISBasePlayer ) )
				player.UpdatePawn( new ISSpectator() );

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

			if ( Client.All.Count < GameConfig.MinimumPlayers )
			{
				HasPrematchStarted = false;
				return;
			}

			if ( !HasPrematchStarted )
			{
				StateEndTime = Time.Now + StateDuration;
				HasPrematchStarted = true;
			}

			if ( Time.Now > StateEndTime )
				OnStateEnded();
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
	}
}
