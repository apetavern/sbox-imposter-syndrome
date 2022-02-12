using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class MenuLeftPanel : Panel
	{
		public MenuLeftPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/MenuLeftPanel.scss" );

			AddChild<MenuPlayerListPanel>();
		}
	}
}
