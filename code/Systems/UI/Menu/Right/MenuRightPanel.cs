using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class MenuRightPanel : Panel
	{
		public MenuRightPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Right/MenuRightPanel.scss" );

			AddChild<ColorSelectionPanel>();
			var logoPanel = Add.Panel( "logo" );
			logoPanel.Add.Label( "https://apetavern.com/", "website" );
		}
	}
}
