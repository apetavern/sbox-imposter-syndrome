using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class PlayerPanel : Panel
	{
		public PlayerPanel( string name )
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/Elements/PlayerPanel.scss" );

			Add.Label( name );
		}
	}
}
