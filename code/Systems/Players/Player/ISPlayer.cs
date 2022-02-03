using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Linq;

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

			Game.Current?.OnKilled( this );

			LifeState = LifeState.Dead;

			RenderColor = Color.White.WithAlpha( 0.4f );

			// Update children render alpha.
			foreach ( var child in Children.Cast<ModelEntity>() )
				child.RenderColor = Color.White.WithAlpha( 0.4f );

			TimeSinceKilled = 0;
		}

		public override void Simulate( Client cl )
		{
			if ( LifeState == LifeState.Dead )
			{
				if ( TimeSinceKilled > 5 && IsServer )
				{
					// Re-enable input
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
