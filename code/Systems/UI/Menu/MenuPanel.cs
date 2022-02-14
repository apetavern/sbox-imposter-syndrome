using ImposterSyndrome.Systems.States;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class MenuPanel : Panel
	{
		public static MenuPanel Instance { get; set; }
		private Label Timer { get; set; }

		public MenuPanel()
		{
			Instance = this;
			StyleSheet.Load( "/Systems/UI/Menu/MenuPanel.scss" );

			AddChild<MenuScene>();
			AddChild<MenuLeftPanel>();
			AddChild<MenuRightPanel>();

			Timer = Add.Label( "", "timer" );

			BindClass( "visible", () => ImposterSyndrome.Instance.CurrentState is WaitingState );
		}

		public override void Tick()
		{
			base.Tick();

			if ( ImposterSyndrome.Instance?.CurrentState is not WaitingState waitingState )
				return;

			if ( !waitingState.HasPrematchStarted )
			{
				if ( Client.All.Count < GameConfig.MinimumPlayers )
					Timer.Text = "WAITING FOR MORE PLAYERS";
				else
					Timer.Text = "WAITING FOR HOST TO START";
				return;
			}

			Timer.Text = $"STARTING IN {MathX.Clamp( (int)(waitingState.StateEndTime - Time.Now), 0, 500 )} SECONDS";
		}
	}
}
