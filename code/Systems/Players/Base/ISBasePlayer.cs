using Sandbox;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISBasePlayer : Player
	{
		public AnimEntity backpack;

		public override void Respawn()
		{
			SetModel( "models/playermodel/terrysus.vmdl" );

			Controller = new ISController();
			Animator = new ISAnimator();
			Camera = new ISCamera();

			backpack = new AnimEntity();
			backpack.SetModel( "models/backpacks/business/susbusinessbackpack.vmdl" );
			backpack.SetParent( this, true );

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}

		public void UpdatePawn( ISBasePlayer newPawn )
		{
			var player = newPawn;
			player.Respawn();

			Client.Pawn = player;

			Delete();
		}
	}
}
