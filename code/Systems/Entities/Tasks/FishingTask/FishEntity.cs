using Sandbox;

namespace ImposterSyndrome.Systems.Entities
{
	public partial class FishEntity : AnimEntity
	{
		private FishShoalEntity ParentShoal { get; set; }
		public bool IsHooked { get; set; }
		private FloatEntity TargetFloat { get; set; }
		private Vector3 TargetPosition { get; set; }
		private float DefaultSwimSpeed { get; set; } = 12f;
		private float SwimSpeed { get; set; }
		private TimeUntil TimeUntilMovementPermitted { get; set; }
		private TimeSince TimeSinceHooked { get; set; }

		public FishEntity AddToShoal( FishShoalEntity shoal )
		{
			SetModel( "models/fish/fish.vmdl" );
			ParentShoal = shoal;

			TargetPosition = PickRandomPosition();
			SwimSpeed = DefaultSwimSpeed;
			Scale = Rand.Float( 1, 1.5f );

			CurrentSequence.Name = "swim";

			Position = shoal.Position + Vector3.Random * 10;
			return this;
		}

		[Event.Tick.Server]
		public void Tick()
		{
			if ( ParentShoal is null || !ParentShoal.IsValid )
				Delete();

			if ( IsHooked )
			{
				if ( TimeSinceHooked > 1 )
				{
					Log.Info( "letting loose" );
					Reset();
				}

				return;
			}

			var moveDir = (TargetPosition - Position).Normal;
			var distance = Vector3.DistanceBetween( TargetPosition, Position );

			if ( distance > 4 )
			{
				if ( TimeUntilMovementPermitted > 0 )
					return;

				Position += moveDir * Time.Delta * SwimSpeed;

				if ( TargetFloat is null )
				{
					Rotation = Rotation.LookAt( moveDir, Vector3.Up );
					SwimSpeed = (SwimSpeed - (SwimSpeed / 500)).Clamp( 0.5f, DefaultSwimSpeed );
				}

				return;
			}
			else
			{
				if ( TargetFloat is not null )
				{
					var shouldBite = Rand.Int( 1, 5 ) == 3;

					if ( shouldBite )
						HookTo( TargetFloat );
					else
						TargetPosition = TargetPosition + (Position - TargetPosition).Normal * 20;

					return;
				}


				TargetPosition = PickRandomPosition();
				TimeUntilMovementPermitted = Rand.Float( 0.5f, 2.5f );
				SwimSpeed = DefaultSwimSpeed;
			}
		}

		private Vector3 PickRandomPosition()
		{
			return (ParentShoal.Position + Vector3.Random + Vector3.Random * 50).WithZ( ParentShoal.Position.z + Rand.Int( -5, 5 ) );
		}

		public void NibbleAt( FloatEntity floatEntity )
		{
			TargetFloat = floatEntity;
			TargetPosition = floatEntity.Position;
			Rotation = Rotation.LookAt( (TargetPosition - Position).Normal, Vector3.Up );
			SwimSpeed = 16;
		}

		public void Reset()
		{
			IsHooked = false;
			TargetFloat = null;
			Parent = null;
			TargetPosition = PickRandomPosition();
			SwimSpeed = DefaultSwimSpeed;
			TimeUntilMovementPermitted = 0;
		}

		public void HookTo( FloatEntity floatEntity )
		{
			Parent = floatEntity;
			IsHooked = true;
			TimeSinceHooked = 0;
		}
	}
}
