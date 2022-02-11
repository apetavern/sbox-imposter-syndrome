using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class PlayerPanel : Panel
	{
		private Panel ColorPanel { get; set; }

		public PlayerPanel( string name )
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/Elements/PlayerPanel.scss" );

			Add.Label( name, "name" );
			ColorPanel = Add.Panel( "color" );
		}

		public void UpdateColor( Color color )
		{
			ColorPanel.Style.BackgroundColor = color;
		}
	}
}
