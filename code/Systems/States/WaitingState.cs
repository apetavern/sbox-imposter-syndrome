using ImposterSyndrome.Systems.Players;
using Sandbox;
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

			foreach ( var player in Client.All.Select( cl => cl.Pawn as ISBasePlayer ) )
				player.UpdatePawn( new ISSpectator() );

			DoPostGameCleanup();
		}

		public override void OnStateEnded()
		{
			base.OnStateEnded();

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
			// Cleanup bodies.
			Entity.All.OfType<ISPlayer>().ToList().ForEach( player => player.Delete() );
		}
	}
}
