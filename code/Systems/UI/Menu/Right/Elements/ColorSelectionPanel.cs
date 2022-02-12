using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class ColorSelectionPanel : Panel
	{
		public static ColorSelectionPanel Instance { get; set; }
		private List<ColorPanel> ColorPanels { get; set; } = new();

		public ColorSelectionPanel()
		{
			Instance = this;

			StyleSheet.Load( "/Systems/UI/Menu/Right/Elements/ColorSelectionPanel.scss" );

			Add.Label( "Pick a color", "title" );

			var colors = Add.Panel( "colors" );

			for ( int i = 0; i < GameConfig.AvailablePlayerColors.Length; i++ )
			{
				var colorPanel = new ColorPanel( i );
				colors.AddChild( colorPanel );

				ColorPanels.Add( colorPanel );
			}
		}

		public void MarkColorUsable( int colourIndex, bool isUsable )
		{
			var targetPanel = ColorPanels.FirstOrDefault( panel => panel.ColorIndex == colourIndex );

			if ( targetPanel is null )
				return;

			targetPanel.IsUsable = isUsable;
		}
	}
}
