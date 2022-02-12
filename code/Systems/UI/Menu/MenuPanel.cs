using ImposterSyndrome.Systems.States;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class MenuPanel : Panel
	{
		public static MenuPanel Instance { get; set; }

		public MenuPanel()
		{
			Instance = this;
			StyleSheet.Load( "/Systems/UI/Menu/MenuPanel.scss" );

			AddChild<MenuScene>();
			AddChild<MenuLeftPanel>();
			AddChild<MenuRightPanel>();

			BindClass( "visible", () => ImposterSyndrome.Instance.CurrentState is WaitingState );
		}
	}
}
