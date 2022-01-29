using Sandbox;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISSpectator : Sandbox.Player
	{
		public override void Respawn()
		{
			SetModel( "models/playermodel/terrysus.vmdl" );
			RenderColor = Color.White.WithAlpha( 0.4f );

			Controller = new WalkController();
			Camera = new ThirdPersonCamera();

			EnableAllCollisions = false;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}
	}
}
