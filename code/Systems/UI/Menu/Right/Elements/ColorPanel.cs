using Sandbox.UI;
using Sandbox;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class ColorPanel : Panel
	{
		public int ColorIndex { get; set; }
		public bool IsUsable { get; set; } = true;

		public ColorPanel( int colorIndex )
		{
			StyleSheet.Load( "/Systems/UI/Menu/Right/Elements/ColorPanel.scss" );

			ColorIndex = colorIndex;

			var color = GameConfig.AvailablePlayerColors[colorIndex];
			Style.BackgroundColor = color;

			AddEventListener( "onclick", () => ImposterSyndrome.SelectColor( ColorIndex ) );

			BindClass( "enabled", () => IsUsable );
		}
	}
}
