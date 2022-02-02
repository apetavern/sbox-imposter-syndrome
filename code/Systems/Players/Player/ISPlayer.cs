using ImposterSyndrome.Systems.Entities;
using ImposterSyndrome.Systems.UI;
using Sandbox;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer : ISBasePlayer, IEntityUse
	{
		[Net, Local, Change] public bool IsImposter { get; set; }
		private TimeSince TimeSinceKilled { get; set; }

		public override void OnKilled()
		{
			base.OnKilled();

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
					newPawn.Respawn( Position );

					cl.Pawn = newPawn;
				}

				return;
			}

			var controller = GetActiveController();
			controller?.Simulate( cl, this, GetActiveAnimator() );
		}

		public bool IsUsable( ISPlayer user, UseType useType )
		{
			// Imposter killing
			if ( useType == UseType.Kill && user.IsImposter && LifeState == LifeState.Alive )
				return true;

			if ( useType != UseType.Report )
				return false;

			// Player reporting
			return LifeState == LifeState.Dead;
		}

		public bool OnUse( ISPlayer user, UseType useType )
		{
			switch ( useType )
			{
				case UseType.Kill:
					OnKilled();
					break;
				case UseType.Report:
					Log.Info( "DEAD BODY REPORTED" );
					break;
			}

			return false;
		}

		public void OnIsImposterChanged()
		{
			PlayerHudEntity.Rebuild();
		}
	}
}
