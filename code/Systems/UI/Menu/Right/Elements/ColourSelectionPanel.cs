using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class ColourSelectionPanel : Panel
	{
		public ColourSelectionPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Right/Elements/ColourSelectionPanel.scss" );

			Add.Label( "Available colours", "title" );
			var colours = Add.Panel( "colours" );

			foreach ( var colour in GameConfig.AvailablePlayerColors )
			{
				var colourPanel = new ColourPanel( colour );
				colours.AddChild( colourPanel );
			}
		}
	}
}
