using System.Linq;
using Sandbox;
using ImposterSyndrome.Systems.Players;

namespace ImposterSyndrome.Systems.States
{
	public partial class PlayingState : BaseState
	{
		[Net] public override string StateName => "PlayingState";
		public override float StateDuration { get; set; } = 30;

		public override void OnStateStarted()
		{
			base.OnStateStarted();

			foreach ( var player in Client.All.Select( cl => cl.Pawn as ISBasePlayer ) )
				player.UpdatePawn( new ISPlayer() );
		}

		public override void OnStateEnded()
		{
			base.OnStateEnded();
			Game.UpdateState( new GameEndState() );
		}
	}
}
