using Sandbox.UI;
using Sandbox;

namespace ImposterSyndrome.Systems.UI
{
	public class ColorPanel : Panel
	{
		private int ColorIndex { get; set; }

		public ColorPanel( int colorIndex )
		{
			StyleSheet.Load( "/Systems/UI/Menu/Right/Elements/ColorPanel.scss" );

			ColorIndex = colorIndex;

			var color = GameConfig.AvailablePlayerColors[colorIndex];
			Style.BackgroundColor = color;

			Log.Info( $"Created new button with color index {ColorIndex}" );

			AddEventListener( "onclick", () => ImposterSyndrome.AssignColorToClient( Local.Client, ColorIndex ) );
		}
	}
}
