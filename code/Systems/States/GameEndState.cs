using Sandbox;

namespace ImposterSyndrome.Systems.States
{
	public enum GameEndReason
	{
		TeamWin,
		NotEnoughPlayers
	}

	public partial class GameEndState : BaseState
	{
		[Net] public override string StateName => "Game end";
		public override float StateDuration { get; set; } = 30;
		[Net] public GameEndReason EndReason { get; set; }

		public GameEndState SetReason( GameEndReason reason )
		{
			EndReason = reason;
			return this;
		}

		public override void OnStateStarted()
		{
			base.OnStateStarted();

			Log.Info( $"Game ended. Reason {EndReason}." );
		}

		public override void OnStateEnded()
		{
			base.OnStateEnded();

			Game.UpdateState( new WaitingState() );
		}
	}
}
