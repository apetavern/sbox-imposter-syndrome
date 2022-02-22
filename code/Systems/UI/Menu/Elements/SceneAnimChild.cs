using Sandbox;

namespace ImposterSyndrome.Systems.UI
{
	public class SceneAnimChild : SceneModel
	{
		private TimeUntil TimeUntilMovementAllowed { get; set; }
		private float MovementSpeed { get; set; } = 60f;
		private BBox MovementBounds { get; set; }
		private bool CanWander { get; set; }
		private bool IsMoving { get; set; }
		private bool NeedsToReturn { get; set; }
		private Vector3 EndPosition { get; set; }
		public SceneModel Backpack { get; set; }

		public SceneAnimChild( SceneWorld sceneWorld, string model, Transform transform ) : base( sceneWorld, model, transform )
		{
			if ( model == "models/playermodel/terrysus.vmdl" )
			{
				Backpack = new SceneModel( sceneWorld, "models/backpacks/business/susbusinessbackpack.vmdl", Transform );
				AddChild( "clothing", Backpack );
			}
		}

		public SceneAnimChild EnableWanderWithin( BBox sceneBounds )
		{
			MovementBounds = sceneBounds;

			CanWander = true;
			return this;
		}

		private Vector3 PickRandomPointWithinBounds()
		{
			return MovementBounds.RandomPointInside.WithZ( 0 );
		}

		public void Tick()
		{
			Move();

			if ( Backpack is null )
				return;

			Backpack.Update( Time.Delta );
		}

		public void ShowBackpack( bool shouldShow )
		{
			if ( Backpack is null )
				return;

			if ( shouldShow )
			{
				Rotation = Rotation.FromYaw( 30 );
				NeedsToReturn = true;

				return;
			}

			if ( NeedsToReturn )
			{
				Rotation = Rotation.FromYaw( 150 );
				NeedsToReturn = false;
			}
		}

		private void Move()
		{
			Animate();

			if ( !CanWander )
				return;

			if ( EndPosition == default )
				EndPosition = PickRandomPointWithinBounds();

			var moveDir = (EndPosition - Position).Normal;

			if ( Vector3.DistanceBetween( Position, EndPosition ) > 10 && TimeUntilMovementAllowed < 0 )
			{
				Position += moveDir * MovementSpeed * Time.Delta;
				Rotation = Rotation.LookAt( moveDir, Vector3.Up );
				IsMoving = true;
			}
			else
			{
				EndPosition = PickRandomPointWithinBounds();
				IsMoving = false;
				TimeUntilMovementAllowed = Rand.Float( 5 );
			}
		}

		public void DoManualRotation( bool manualMove )
		{
			if ( manualMove )
				Rotation *= Rotation.FromYaw( Mouse.Delta.x );
		}

		public void Animate()
		{
			Update( Time.Delta );

			SetAnimParameter( "grounded", true );
			SetAnimParameter( "walking", IsMoving );
		}
	}
}
