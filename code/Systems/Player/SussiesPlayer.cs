using Sandbox;

namespace Sussies.Systems.Player
{
	public partial class SussiesPlayer : Sandbox.Player
	{
		public override void Respawn()
		{
			SetModel( "models/playermodel/terrysus.vmdl" );

			Controller = new WalkController();
			Animator = new SussiesAnimator();
			Camera = new SussiesCamera();

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}
	}
}
