using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISSpectator : ISBasePlayer
	{
		public void Respawn( ISBasePlayer oldPawn = null )
		{
			if ( oldPawn is null )
				Game.Current?.MoveToSpawnpoint( this );
			else
			{
				Position = oldPawn.Position;
				TakeAllTasksFrom( oldPawn );
			}

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
		}
	}
}
