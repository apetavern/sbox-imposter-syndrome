using Sandbox;

namespace Sussies.Systems.Player
{
	public partial class SussiesCamera : Camera
	{
		private Angles orbitAngles;
		private float orbitDistance = 200;

		public override void Update()
		{
			var pawn = Local.Pawn as AnimEntity;

			Position = pawn.Position;
			Vector3 targetPos;

			Position += Vector3.Up * (60 * pawn.Scale);
			Rotation = Rotation.From( orbitAngles );

			targetPos = Position + Rotation.Backward * orbitDistance;

			var tr = Trace.Ray( Position, targetPos ).WorldOnly()
				.Radius( 1 )
				.Run();

			Position = tr.EndPos;
			FieldOfView = 90;

			Viewer = null;
		}

		public override void BuildInput( InputBuilder input )
		{
			orbitDistance -= input.MouseWheel * 10;
			orbitDistance = orbitDistance.Clamp( 100, 300 );

			orbitAngles.yaw += input.AnalogLook.yaw;
			orbitAngles.pitch += input.AnalogLook.pitch;
			orbitAngles = orbitAngles.Normal;
			orbitAngles.pitch = orbitAngles.pitch.Clamp( 35, 60 );

			input.ViewAngles.yaw = orbitAngles.yaw;
			input.ViewAngles.pitch = 10f;

			input.Clear();
			input.StopProcessing = true;

			base.BuildInput( input );
		}
	}
}
