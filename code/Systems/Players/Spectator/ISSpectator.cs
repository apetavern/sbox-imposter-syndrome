using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISSpectator : ISBasePlayer
	{
		public override void Respawn()
		{
			base.Respawn();

			RenderColor = Color.White.WithAlpha( 0.4f );

			// Update children render alpha.
			foreach ( var child in Children.Cast<ModelEntity>() )
				child.RenderColor = Color.White.WithAlpha( 0.4f );

			EnableAllCollisions = false;
		}
	}
}
