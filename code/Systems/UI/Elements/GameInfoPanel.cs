using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	[UseTemplate]
	public class GameInfoPanel : Panel
	{
		public GameInfoPanel()
		{
			StyleSheet.Load( "/Systems/UI/StateInfoPanel.scss" );
		}
	}
}
