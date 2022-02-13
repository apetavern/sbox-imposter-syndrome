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
		private Vector3 EndPosition { get; set; }
		private Rotation DefaultRotation { get; set; }
		private bool ShowBackpackToCamera { get; set; }
		private bool IsPlayerAvatar { get; set; }
		public AnimSceneObject Backpack { get; set; }

		public SceneAnimChild( Model model, Transform transform ) : base( model, transform )
		{
			DefaultRotation = Rotation.FromYaw( 160 );

			if ( model.Name == "models/playermodel/terrysus.vmdl" )
			{
				var backbackModel = Model.Load( "models/backpacks/business/susbusinessbackpack.vmdl" );
				Backpack = new AnimSceneObject( backbackModel, Transform );
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

			if ( !IsPlayerAvatar )
				return;

			if ( ShowBackpackToCamera && Rotation != Rotation.FromYaw( 10 ) )
			{
				Rotation *= Rotation.FromYaw( 10 );
				return;
			}

			if ( !ShowBackpackToCamera && Rotation != DefaultRotation )
			{
				Rotation *= Rotation.FromYaw( 10 );
			}
		}

		public void ShowBackpack( bool shouldShow )
		{
			if ( Backpack is null )
				return;

			ShowBackpackToCamera = shouldShow;
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

		public SceneAnimChild MarkAsPlayerAvatar()
		{
			IsPlayerAvatar = true;
			return this;
		}

		public void Animate()
		{
			Update( Time.Delta );
			SetAnimBool( "grounded", true );
			SetAnimBool( "walking", IsMoving );
		}
	}
}
