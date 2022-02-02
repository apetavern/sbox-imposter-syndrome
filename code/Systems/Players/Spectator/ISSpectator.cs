using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISSpectator : ISBasePlayer
	{
		public void Respawn( Vector3 position = default )
		{
			if ( position == default )
				Game.Current?.MoveToSpawnpoint( this );
			else
				Position = position;

			ResetInterpolation();

			SetModel( "models/playermodel/terrysus.vmdl" );
			backpack = new AnimEntity();
			backpack.SetModel( "models/backpacks/business/susbusinessbackpack.vmdl" );
			backpack.SetParent( this, true );

			// TODO: A noclip walk controller?
			Controller = new ISController();
			Animator = new ISAnimator();
			Camera = new ISCamera();

			LifeState = LifeState.Alive;
			Health = 100;
			Velocity = Vector3.Zero;
			WaterLevel.Clear();

			EnableAllCollisions = false;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			UpdateRenderAlpha();
		}

		private void UpdateRenderAlpha()
		{
			RenderColor = Color.White.WithAlpha( 0.4f );

			// Update children render alpha.
			foreach ( var child in Children.Cast<ModelEntity>() )
				child.RenderColor = Color.White.WithAlpha( 0.4f );
		}
	}
}
