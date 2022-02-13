using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.UI
{
	public class MenuScene : Panel
	{
		public static MenuScene Instance { get; set; }
		private ScenePanel RenderScene { get; set; }
		private Vector3 RenderScenePos => new Vector3( 0, 0, 20 );
		private Angles RenderSceneAngles { get; set; } = new( 0f, 0.0f, 0.0f );
		private Vector3 PlayerPosition => new Vector3( 80, -10, 0 );
		private BBox SceneAnimBounds { get; set; }
		public SceneAnimChild Player { get; set; }
		private List<SceneAnimChild> SceneAnimChildren { get; set; } = new();

		public MenuScene()
		{
			Instance = this;
			SceneAnimBounds = new BBox( PlayerPosition + Vector3.One * 120, PlayerPosition - Vector3.One * 120 );

			Build();
		}

		public override void OnButtonEvent( ButtonEvent e )
		{
			if ( e.Button == "mouseleft" )
			{
				RenderScene.SetMouseCapture( e.Pressed );
			}

			base.OnButtonEvent( e );
		}

		private void AddSceneAnimChildren()
		{
			Player = new SceneAnimChild( Model.Load( "models/playermodel/terrysus.vmdl" ),
					Transform.Zero.WithScale( 1.2f )
					.WithPosition( PlayerPosition )
					.WithRotation( Rotation.From( 0, -200, 0 ) ) ).MarkAsPlayerAvatar();
			SceneAnimChildren.Add( Player );

			SceneAnimChildren.Add(
			new SceneAnimChild( Model.Load( "models/playermodel/terrysus.vmdl" ),
				Transform.Zero.WithScale( 1f )
				.WithPosition( new Vector3( -120, 120, -4 ) ) )
				.EnableWanderWithin( SceneAnimBounds )
			);
		}

		public override void Tick()
		{
			base.Tick();

			if ( RenderScene is null )
				return;

			foreach ( var child in SceneAnimChildren )
				child.Tick();

			if ( RenderScene.HasMouseCapture )
				Player.Rotation *= Rotation.FromYaw( Mouse.Delta.x );
		}

		private void Reset()
		{
			RenderScene?.Delete( true );
			SceneAnimChildren?.Clear();
		}

		public void Build()
		{
			Reset();

			using ( SceneWorld.SetCurrent( new SceneWorld() ) )
			{
				SceneObject.CreateModel( Model.Load( "models/menus/menuscene/menuscene.vmdl" ), Transform.Zero.WithScale( 1 ) );

				AddSceneAnimChildren();

				Light.Point( Vector3.Up * 150.0f, 300.0f, Color.White * 5.0f );
				Light.Point( Vector3.Up * 75.0f + Vector3.Forward * 100.0f, 200, Color.White * 15.0f );
				Light.Point( Vector3.Up * 75.0f + Vector3.Left * 100.0f, 200, GetSkyColor() * 20.0f );
				Light.Point( Vector3.Up * 75.0f + Vector3.Right * 100.0f, 200, Color.White * 15.0f );
				Light.Point( Vector3.Up * 100.0f + Vector3.Up, 200, Color.White * 35.0f );

				RenderScene = Add.ScenePanel( SceneWorld.Current, RenderScenePos, Rotation.From( RenderSceneAngles ), 75 );

				RenderScene.Style.Width = Length.Percent( 100 );
				RenderScene.Style.Height = Length.Percent( 100 );

				RenderScene.AmbientColor = new Color( .25f, .15f, .15f ) * 0.5f;
			}
		}

		public void UpdateBackpackColor( int colorIndex )
		{
			if ( Player.Backpack is null )
				return;

			Player.Backpack.ColorTint = GameConfig.AvailablePlayerColors[colorIndex];
		}

		private Color GetSkyColor()
		{
			var skyColor = Color.White;
			var sceneLight = Entity.All.FirstOrDefault( x => x is EnvironmentLightEntity ) as EnvironmentLightEntity;

			if ( sceneLight.IsValid() )
				skyColor = sceneLight.SkyColor;

			return skyColor;
		}

		public override void OnHotloaded()
		{
			base.OnHotloaded();

			Build();
		}
	}
}
