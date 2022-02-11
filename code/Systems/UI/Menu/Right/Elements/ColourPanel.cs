using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class ColourPanel : Panel
	{
		public ColourPanel( Color panelColor )
		{
			StyleSheet.Load( "/Systems/UI/Menu/Right/Elements/ColourPanel.scss" );

			Style.BackgroundColor = panelColor;
		}
	}
}
