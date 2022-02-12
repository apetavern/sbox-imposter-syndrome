using Sandbox.UI;
using Sandbox.UI.Construct;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class PlayerPanel : Panel
	{
		private Client HeldClient { get; set; }
		private Panel ColorPanel { get; set; }

		public PlayerPanel( int clientNetIdent )
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/Elements/PlayerPanel.scss" );

			HeldClient = Client.All.FirstOrDefault( x => x.NetworkIdent == clientNetIdent );

			var playerInfo = Add.Panel( "playerinfo" );
			playerInfo.Add.Image( $"avatar:{HeldClient.PlayerId}", "avatar" );
			playerInfo.Add.Label( HeldClient.Name, "name" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( ImposterSyndrome.Instance.AssignedColors is null )
				return;

			if ( ColorPanel is null )
			{
				ColorPanel = Add.Panel( "color" );
			}

			if ( !ImposterSyndrome.Instance.AssignedColors.Any( x => x.Key == HeldClient ) )
				return;

			var colorIndex = ImposterSyndrome.Instance.AssignedColors.FirstOrDefault( x => x.Key == HeldClient ).Value;

			if ( ColorPanel.ComputedStyle?.BackgroundColor != GameConfig.AvailablePlayerColors[colorIndex] )
			{
				ColorPanel.Style.BackgroundColor = GameConfig.AvailablePlayerColors[colorIndex];
				ColorPanel.Style.BackgroundImage = Texture.Transparent;
			}
		}
	}
}
