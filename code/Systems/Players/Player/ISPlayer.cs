using Sandbox;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer : Sandbox.Player
	{
		public AnimEntity backpack;

		public override void Respawn()
		{
			SetModel( "models/playermodel/terrysus.vmdl" );

			Controller = new WalkController();
			Animator = new ISAnimator();
			Camera = new ISCamera();

			backpack = new AnimEntity();
			backpack.SetModel( "models/backpacks/business/susbusinessbackpack.vmdl" );
			backpack.SetParent( this, true );
			backpack.RenderColor = Color.Random.WithAlpha( 1 );

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}
	}
}
