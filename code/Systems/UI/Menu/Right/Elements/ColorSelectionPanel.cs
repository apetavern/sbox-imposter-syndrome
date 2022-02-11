using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class ColorSelectionPanel : Panel
	{
		public ColorSelectionPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Right/Elements/ColorSelectionPanel.scss" );

			Add.Label( "Pick your color", "title" );

			var colors = Add.Panel( "colors" );

			for ( int i = 0; i < GameConfig.AvailablePlayerColors.Length; i++ )
			{
				var colorPanel = new ColorPanel( i );
				colors.AddChild( colorPanel );
			}
		}
	}
}
