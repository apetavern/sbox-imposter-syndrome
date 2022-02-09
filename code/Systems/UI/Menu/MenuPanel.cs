using ImposterSyndrome.Systems.States;
using Sandbox.UI;
using System.ComponentModel;

namespace ImposterSyndrome.Systems.UI
{
	public class MenuPanel : Panel
	{
		public static MenuPanel Instance { get; set; }

		public MenuPanel()
		{
			Instance = this;
			StyleSheet.Load( "/Systems/UI/Menu/MenuPanel.scss" );

			AddChild<MenuPlayersPanel>();

			BindClass( "visible", () => ImposterSyndrome.Instance.CurrentState is WaitingState );
		}
	}
}
