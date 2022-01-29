using ImposterSyndrome.Systems.Players;
using Sandbox;

namespace ImposterSyndrome.Systems.States
{
	public partial class WaitingState : BaseState
	{
		[Net] public override string StateName => "Waiting for players";
		public override float StateDuration { get; set; } = 30;

		public override void OnStateEnded()
		{
			foreach ( var player in Client.All )
			{
				player.Pawn?.Delete();

				var newPawn = new ISPlayer();
				newPawn.Respawn();
				player.Pawn = newPawn;
			}

			base.OnStateEnded();
		}
	}
}
