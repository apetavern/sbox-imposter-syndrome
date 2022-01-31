using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System.Collections.Generic;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISBasePlayer : Sandbox.Player
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
