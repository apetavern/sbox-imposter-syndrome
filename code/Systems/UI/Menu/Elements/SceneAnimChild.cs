using Sandbox;

namespace ImposterSyndrome.Systems.UI
{
	public class SceneAnimChild : AnimSceneObject
	{
		private TimeUntil TimeUntilMovementAllowed { get; set; }
		private float MovementSpeed { get; set; } = 60f;
		private BBox MovementBounds { get; set; }
		private bool CanWander { get; set; }
		private bool IsMoving { get; set; }
		private Vector3 StartPosition { get; set; }
		private Vector3 EndPosition { get; set; }

		public SceneAnimChild( Model model, Transform transform ) : base( model, transform )
		{
			StartPosition = transform.Position;
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

		public void Animate()
		{
			Update( Time.Delta );
			SetAnimBool( "grounded", true );
			SetAnimBool( "walking", IsMoving );
		}
	}
}
