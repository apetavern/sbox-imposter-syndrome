using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class MenuLeftPanel : Panel
	{
		public MenuLeftPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/MenuLeftPanel.scss" );

			Add.Panel( "logo" );
			AddChild<PlayerListPanel>();
		}
	}
}
