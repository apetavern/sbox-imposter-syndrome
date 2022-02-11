using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class PlayerListPanel : Panel
	{
		public PlayerListPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/Elements/PlayerListPanel.scss" );

			Add.Label( "These are players" );
		}
	}
}
