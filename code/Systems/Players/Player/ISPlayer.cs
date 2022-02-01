using ImposterSyndrome.Systems.Entities;
using ImposterSyndrome.Systems.UI;
using Sandbox;
using Sandbox.Internal;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer : ISBasePlayer, IEntityUse
	{
		[Net, Local, Change] public bool IsImposter { get; set; }
		private TimeSince TimeSinceKilled { get; set; }

		public override void OnKilled()
		{
			base.OnKilled();

			TimeSinceKilled = 0;
		}

		public override void Simulate( Client cl )
		{
			if ( LifeState == LifeState.Dead )
			{
				if ( TimeSinceKilled > 3 && IsServer )
				{
					// Make this player a spectator.
					var newPawn = new ISSpectator();
					newPawn.Respawn();

					cl.Pawn = newPawn;
				}

				return;
			}

			var controller = GetActiveController();
			controller?.Simulate( cl, this, GetActiveAnimator() );
		}

		public override void Respawn()
		{
			base.Respawn();
		}

		public bool IsUsable( ISPlayer user )
		{
			return LifeState == LifeState.Dead;
		}

		public bool OnUse( ISPlayer user )
		{
			Log.Info( $"Report death of {user}" );

			return false;
		}

		public void OnIsImposterChanged()
		{
			PlayerHudEntity.Rebuild();
		}
	}
}
