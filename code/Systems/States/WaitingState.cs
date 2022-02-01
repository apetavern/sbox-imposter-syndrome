using ImposterSyndrome.Systems.Players;
using Sandbox;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
			base.OnStateStarted();

			ImposterSyndrome.Instance?.Players?.Clear();

			foreach ( var player in Client.All.Select( cl => cl.Pawn as ISBasePlayer ) )
				player.UpdatePawn( new ISSpectator() );
		}

		public override void OnStateEnded()
		{
			base.OnStateEnded();

			ImposterSyndrome.UpdateState( new PlayingState() );
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
	}
}
