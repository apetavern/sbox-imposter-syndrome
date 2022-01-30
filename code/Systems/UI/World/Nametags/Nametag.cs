using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ImposterSyndrome.Systems.Players;

namespace ImposterSyndrome.Systems.UI
{
	public class Nametag : WorldPanel
	{
		public ISBasePlayer Player { get; set; }

		private Label Name;

		public Nametag()
		{
			StyleSheet.Load( "/Systems/UI/World/Nametags/Nametag.scss" );

			Name = Add.Label( "Name" );

			float width = 1000;
			float height = 250;
			PanelBounds = new Rect( -width / 2, -height / 2, width, height );

			SceneObject.ZBufferMode = ZBufferMode.None;
			SceneObject.Flags.BloomLayer = false;
		}

		private void Move()
		{
			if ( !Player.IsValid() || Player is null )
			{
				Delete( true );
				return;
			}

			Name.Text = Player.Client.Name;

			// Temporarily offset the height of the world panel, just until the customer controller (with an appropriate eye height) is made.
			var tempOffset = Vector3.Down * 20;
			Position = Player.EyePos + tempOffset;

			Rotation = Local.Pawn.EyeRot.RotateAroundAxis( Vector3.Up, 180 );
		}

		public override void Tick()
		{
			base.Tick();

			Move();
		}
	}
}
