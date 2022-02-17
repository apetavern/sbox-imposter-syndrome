using ImposterSyndrome.Systems.Players;
using Sandbox;

namespace ImposterSyndrome.Systems.Entities
{
	public partial class FloatEntity : AnimEntity
	{
		public bool IsFloating { get; set; }
		public FishShoalEntity Shoal { get; set; }
		public float Speed { get; set; }
		public TimeUntil TimeUntilFishBite { get; set; }
		public FishEntity AttractedFish { get; set; }
		private bool IsReeling { get; set; }

		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/float/float.vmdl" );
		}

		public bool Reel()
		{
			IsReeling = true;
			IsFloating = false;
			Velocity = 0;
			DeleteAsync( 1 );

			if ( AttractedFish is not null )
				AttractedFish.HookLocked = true;

			return AttractedFish is null ? false : AttractedFish.IsHooked;
		}

		public void Cleanup()
		{
			AttractedFish?.Reset();
		}

		[Event.Tick.Server]
		public void Tick()
		{
			if ( IsFloating )
			{
				// Do some bobbing?
				if ( TimeUntilFishBite > 0 || AttractedFish is not null )
					return;
				else
				{
					// Pick a random fish
					AttractedFish = Rand.FromList( Shoal.Fish );
					AttractedFish.NibbleAt( this );
				}

				return;
			}

			var direction = IsReeling ? -Owner.Rotation.Forward : Owner.Rotation.Forward;
			var gravity = IsReeling ? -PhysicsWorld.Gravity : PhysicsWorld.Gravity;

			Velocity += Speed * direction * Time.Delta;
			Velocity += gravity * 0.5f * Time.Delta;

			var target = Position + Velocity * Time.Delta;

			TraceResult tr;

			if ( !IsReeling )
				tr = Trace.Ray( Position, target ).HitLayer( CollisionLayer.Water ).Ignore( Owner ).Ignore( Shoal ).Run();
			else
				tr = Trace.Ray( Position, target ).EntitiesOnly().Ignore( Shoal ).Ignore( AttractedFish ).Run();

			if ( tr.Hit )
			{
				IsFloating = true;
				TimeUntilFishBite = Rand.Float( 3, 5 );

				if ( tr.Entity is ISPlayer )
					Delete();

				return;
			}

			Position = target;
		}

		[ClientRpc]
		public void HookedEffects( bool isHooked )
		{
			SetAnimBool( "bite", isHooked );
		}

		[ClientRpc]
		public void NibbleEffects()
		{
			SetAnimBool( "nibble", true );
		}
	}
}
