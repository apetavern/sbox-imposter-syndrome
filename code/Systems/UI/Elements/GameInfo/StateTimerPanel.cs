using Sandbox;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	[UseTemplate]
	public class StateTimerPanel : Panel
	{
		public Label StateTimerLabel { get; set; }

		public StateTimerPanel()
		{
			StyleSheet.Load( "/Systems/UI/GameInfoPanel.scss" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( ImposterSyndrome.Instance.CurrentState is null )
				return;

			StateTimerLabel.Text = $"{MathX.Clamp( (int)(ImposterSyndrome.Instance.CurrentState.StateEndTime - Time.Now), 0, 500 ).ToString()}s";
		}
	}
}
