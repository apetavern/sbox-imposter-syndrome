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

			Add.Label( HeldClient.Name, "name" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( ImposterSyndrome.Instance.AssignedColors is null )
				return;

			var color = ImposterSyndrome.Instance.AssignedColors.FirstOrDefault( x => x.Key == HeldClient ).Value;

			if ( ColorPanel is null || ColorPanel.ComputedStyle.BackgroundColor != GameConfig.AvailablePlayerColors[color] )
			{
				ColorPanel?.Delete();

				ColorPanel = Add.Panel( "color" );
				ColorPanel.Style.BackgroundColor = GameConfig.AvailablePlayerColors[color];
			}
		}
	}
}
