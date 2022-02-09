using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Linq;

namespace ImposterSyndrome.Systems.UI
{
	public class WorldPreviewScene : Panel
	{
		private ScenePanel RenderScene { get; set; }
		private Angles RenderSceneAngles { get; set; } = new( 35.0f, 0.0f, 0.0f );
		private Vector3 RenderScenePos => new Vector3( -100, -50, 25 );
		private Transform PlayerTransform { get; set; }
		private AnimSceneObject Player { get; set; }

		public override void Tick()
		{
			base.Tick();

			if ( RenderScene == null )
				return;

			Animate();
		}

		public WorldPreviewScene()
		{
			PlayerTransform = Transform.Zero.WithScale( 1f )
					.WithPosition( new Vector3( -64, 32, -4 ) )
					.WithRotation( Rotation.From( 0, -130, 0 ) );

			Build();
		}

		public override void OnHotloaded()
		{
			base.OnHotloaded();

			Build();
		}

		public void Animate()
		{
			Player.Update( Time.Delta );
			Player.SetAnimBool( "grounded", true );
		}

		public void Build()
		{
			RenderScene?.Delete( true );

			using ( SceneWorld.SetCurrent( new SceneWorld() ) )
			{
				SceneObject.CreateModel( Model.Load( "models/avatareditorscene.vmdl" ), Transform.Zero.WithScale( 1 ).WithPosition( Vector3.Down * 4 ) );

				Player = new AnimSceneObject( Model.Load( "models/playermodel/terrysus.vmdl" ), PlayerTransform );

				Light.Point( Vector3.Up * 150.0f, 200.0f, Color.White * 5.0f );
				Light.Point( Vector3.Up * 75.0f + Vector3.Forward * 100.0f, 200, Color.White * 15.0f );
				Light.Point( Vector3.Up * 75.0f + Vector3.Backward * 100.0f, 200, Color.White * 15f );
				Light.Point( Vector3.Up * 75.0f + Vector3.Left * 100.0f, 200, GetSkyColor() * 20.0f );
				Light.Point( Vector3.Up * 75.0f + Vector3.Right * 100.0f, 200, Color.White * 15.0f );
				Light.Point( Vector3.Up * 100.0f + Vector3.Up, 200, Color.Yellow * 15.0f );

				RenderScene = Add.ScenePanel( SceneWorld.Current, RenderScenePos, Rotation.From( RenderSceneAngles ), 75 );

				RenderScene.Style.Width = Length.Percent( 100 );
				RenderScene.Style.Height = Length.Percent( 100 );

				RenderScene.CameraRotation = Rotation.From( 0, 75, 0 );
				RenderSceneAngles = RenderScene.CameraRotation.Angles();

				RenderScene.AmbientColor = new Color( .25f, .15f, .15f ) * 0.5f;
			}
		}

		private Color GetSkyColor()
		{
			var skyColor = Color.White;
			var sceneLight = Entity.All.FirstOrDefault( x => x is EnvironmentLightEntity ) as EnvironmentLightEntity;

			if ( sceneLight.IsValid() )
				skyColor = sceneLight.SkyColor;

			return skyColor;
		}
	}
}
