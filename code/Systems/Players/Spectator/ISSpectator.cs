using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISSpectator : ISBasePlayer
	{
		public override void Respawn()
		{
			base.Respawn();

			SetModel( "models/playermodel/terrysus.vmdl" );
			backpack = new AnimEntity();
			backpack.SetModel( "models/backpacks/business/susbusinessbackpack.vmdl" );
			backpack.SetParent( this, true );

			// TODO: A noclip walk controller?
			Controller = new ISController();
			Animator = new ISAnimator();
			Camera = new ISCamera();

			EnableAllCollisions = false;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
		}
	}
}
