using Sandbox;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	public partial class MenuHudEntity : HudEntity<RootPanel>
	{
		public static MenuHudEntity Instance { get; set; }

		public MenuHudEntity()
		{
			Instance = this;

			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( "/Systems/UI/PlayerHudEntity.scss" );

			RootPanel.AddChild<DevMenu>();
		}
	}
}
