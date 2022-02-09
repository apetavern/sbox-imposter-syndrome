using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class MenuPlayersPanel : Panel
	{
		public MenuPlayersPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/Elements/MenuPlayersPanel.scss" );

			Add.Label( "These are players" );
		}
	}
}
