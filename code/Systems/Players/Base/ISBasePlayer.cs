using ImposterSyndrome.Systems.Entities;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISBasePlayer : Player, IEntityUse
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

		public override void CreateHull()
		{
			CollisionGroup = CollisionGroup.Player;
			AddCollisionLayer( CollisionLayer.Player );
			SetupPhysicsFromAABB( PhysicsMotionType.Keyframed, new Vector3( -8, -8, 0 ), new Vector3( 8, 8, 30 ) );

			MoveType = MoveType.MOVETYPE_WALK;
			EnableHitboxes = true;
		}

		public void UpdatePawn( ISBasePlayer newPawn )
		{
			var player = newPawn;
			player.Respawn();

			Client.Pawn = player;

			Delete();
		}

		public bool IsUsable( ISBasePlayer user, UseType useType )
		{
			// Imposter killing
			if ( useType == UseType.Kill && (user as ISPlayer).IsImposter && LifeState == LifeState.Alive )
				return true;

			// Player reporting
			return useType == UseType.Report && LifeState == LifeState.Dead;
		}

		public bool OnUse( ISBasePlayer user, UseType useType )
		{
			switch ( useType )
			{
				case UseType.Kill:
					OnKilled();
					break;
				case UseType.Report:
					Log.Info( "DEAD BODY REPORTED" );
					break;
			}

			return false;
		}

		[ServerCmd]
		public static void KillTarget( string name )
		{
			var player = Client.All.FirstOrDefault( x => x.Name == name ).Pawn;
			player.OnKilled();
		}
	}
}
