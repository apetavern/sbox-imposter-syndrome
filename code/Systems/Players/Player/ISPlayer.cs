using ImposterSyndrome.Systems.UI;
using Sandbox;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer : ISBasePlayer
	{
		[Net, Local, Change] public bool IsImposter { get; set; }
		private TimeSince TimeSinceKilled { get; set; }

		public override void OnKilled()
		{
			base.OnKilled();

			PhysicsClear();
			Rotation = Rotation.LookAt( Vector3.Up );

			TimeSinceKilled = 0;
		}

		public override void Simulate( Client cl )
		{
			if ( LifeState == LifeState.Dead )
			{
				if ( TimeSinceKilled > 5 && IsServer )
				{
					var newPawn = new ISSpectator();
					newPawn.Respawn( cl.Pawn as ISBasePlayer );

					cl.Pawn = newPawn;
				}

				return;
			}

			var controller = GetActiveController();
			controller?.Simulate( cl, this, GetActiveAnimator() );
		}

		public void OnIsImposterChanged()
		{
			PlayerHudEntity.Rebuild();
		}
	}
}
