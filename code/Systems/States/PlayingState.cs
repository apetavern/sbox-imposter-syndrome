using System.Linq;
using Sandbox;
using ImposterSyndrome.Systems.Players;

namespace ImposterSyndrome.Systems.States
{
	public partial class PlayingState : BaseState
	{
		[Net] public override string StateName => "Playing";
		public override float StateDuration { get; set; } = 30;

		public override void OnStateStarted()
		{
			base.OnStateStarted();
		}

		public override void OnSecond()
		{
			if ( Host.IsClient )
				return;

			if ( HasMinimumAlivePlayers() || HasTasksOutstanding() )
				ImposterSyndrome.UpdateState( new GameEndState().SetReason( GameEndReason.TeamWin ) );

			if ( Time.Now > StateEndTime )
				OnStateEnded();
		}

		private bool HasMinimumAlivePlayers()
		{
			var players = ImposterSyndrome.Instance.Players;
			var alivePlayers = players.Where( player => !player.IsImposter && player.LifeState == LifeState.Alive );

			if ( alivePlayers.Count() <= 1 )
				return false;

			return true;
		}

		private bool HasTasksOutstanding()
		{
			if ( ISPlayer.GetAllPlayersTaskProgress() <= 100 )
				return false;

			return true;
		}

		public override void OnStateEnded()
		{
			base.OnStateEnded();

			Log.Info( "was this even called" );

			ImposterSyndrome.UpdateState( new VotingState() );
		}
	}
}
