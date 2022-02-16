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

		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/float/float.vmdl" );
		}

		public void Cleanup()
		{
			AttractedFish.Reset();
			Delete();
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

			Velocity += Speed * Owner.Rotation.Forward * Time.Delta;
			Velocity += PhysicsWorld.Gravity * 0.5f * Time.Delta;

			var target = Position + Velocity * Time.Delta;
			var tr = Trace.Ray( Position, target ).HitLayer( CollisionLayer.Water ).Ignore( Owner ).Ignore( Shoal ).Run();

			if ( tr.Hit )
			{
				IsFloating = true;
				TimeUntilFishBite = Rand.Float( 3, 5 );
				return;
			}

			Position = target;
		}
	}
}
