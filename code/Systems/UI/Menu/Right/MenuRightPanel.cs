using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class MenuRightPanel : Panel
	{
		public MenuRightPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Right/MenuRightPanel.scss" );

			AddChild<ColorSelectionPanel>();
			Add.Panel( "logo" );
		}
	}
}
