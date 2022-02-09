using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class ColourSelectionPanel : Panel
	{
		public ColourSelectionPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Right/Elements/ColourSelectionPanel.scss" );

			Add.Label( "Colours" );
		}
	}
}
