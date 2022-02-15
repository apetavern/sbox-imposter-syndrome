using Sandbox;

namespace ImposterSyndrome.Systems.Entities
{
	public partial class FishEntity : AnimEntity
	{
		private FishShoalEntity ParentShoal { get; set; }
		public bool IsHooked { get; set; }
		private Vector3 TargetPosition { get; set; }
		private float DefaultSwimSpeed { get; set; } = 12f;
		private float SwimSpeed { get; set; }
		private TimeUntil TimeUntilMovementPermitted { get; set; }

		public FishEntity AddToShoal( FishShoalEntity shoal )
		{
			SetModel( "models/fish/fish.vmdl" );
			ParentShoal = shoal;

			TargetPosition = PickRandomPosition();
			SwimSpeed = DefaultSwimSpeed;
			Scale = Rand.Float( 1, 1.5f );

			Position = shoal.Position + Vector3.Random * 10;
			return this;
		}

		[Event.Tick.Server]
		public void Tick()
		{
			if ( ParentShoal is null || !ParentShoal.IsValid )
				Delete();

			var moveDir = (TargetPosition - Position).Normal;
			var distance = Vector3.DistanceBetween( TargetPosition, Position );

			if ( distance > 4 )
			{
				if ( TimeUntilMovementPermitted > 0 )
					return;

				Position += moveDir * Time.Delta * SwimSpeed;
				Rotation = Rotation.LookAt( moveDir, Vector3.Up );

				SwimSpeed = (SwimSpeed - (SwimSpeed / 500)).Clamp( 0.5f, DefaultSwimSpeed );

				return;
			}
			else
			{
				TargetPosition = PickRandomPosition();
				TimeUntilMovementPermitted = Rand.Float( 0.5f, 2.5f );
				SwimSpeed = DefaultSwimSpeed;
			}

			// TODO: Lookout for bait in the water, rotate towards and nibble if found
		}

		private Vector3 PickRandomPosition()
		{
			return (ParentShoal.Position + Vector3.Random + Vector3.Random * 50).WithZ( ParentShoal.Position.z + Rand.Int( -5, 5 ) );
		}

		private void HookTo()
		{

		}
	}
}
