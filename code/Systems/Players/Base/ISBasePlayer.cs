using ImposterSyndrome.Systems.Entities;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISBasePlayer : Player
	{
		public AnimEntity Backpack { get; set; }

		public override void Respawn()
		{
			SetModel( "models/playermodel/terrysus.vmdl" );

			Controller = new ISController();
			Animator = new ISAnimator();
			CameraMode = new ISCamera();

			Backpack = new AnimEntity();
			Backpack.SetModel( "models/backpacks/business/susbusinessbackpack.vmdl" );
			Backpack.SetParent( this, true );

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

		public static void ReturnAllToCampfire()
		{
			foreach ( var player in ImposterSyndrome.Instance?.Players.Where( x => x.LifeState == LifeState.Alive ) )
			{
				player.Position = CampfireEntity.Instance?.Position ?? player.Position;
				player.ResetInterpolation();
			}
		}

		protected void UpdateRenderAlpha()
		{
			RenderColor = Color.White.WithAlpha( 0.4f );

			// Update children render alpha.
			foreach ( var child in Children.Cast<ModelEntity>() )
				child.RenderColor = Color.White.WithAlpha( 0.4f );
		}

		[ServerCmd]
		public static void KillTarget( string name )
		{
			var player = Client.All.FirstOrDefault( x => x.Name == name ).Pawn;
			player.OnKilled();
		}
	}
}
