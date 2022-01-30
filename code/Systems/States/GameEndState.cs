using ImposterSyndrome.Systems.Players;
using Sandbox;

namespace ImposterSyndrome.Systems.States
{
	public partial class GameEndState : BaseState
	{
		[Net] public override string StateName => "Game end";
		public override float StateDuration { get; set; } = 30;

		public override void OnStateEnded()
		{
			base.OnStateEnded();

			Game.UpdateState( new WaitingState() );
		}
	}
}
