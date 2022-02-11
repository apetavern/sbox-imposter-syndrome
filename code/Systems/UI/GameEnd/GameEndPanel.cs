using ImposterSyndrome.Systems.States;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI.GameEnd
{
	public class GameEndPanel : Panel
	{
		public static GameEndPanel Instance { get; set; }

		public GameEndPanel()
		{
			Instance = this;
			StyleSheet.Load( "/Systems/UI/GameEnd/GameEndPanel.scss" );

			AddChild<SomethingPanel>();

			BindClass( "visible", () => ImposterSyndrome.Instance.CurrentState is GameEndState );
		}
	}
}
