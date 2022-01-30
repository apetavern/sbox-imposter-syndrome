using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	[UseTemplate]
	public class StateNamePanel : Panel
	{
		public Label StateNameLabel { get; set; }

		public StateNamePanel()
		{
			StyleSheet.Load( "/Systems/UI/StateInfoPanel.scss" );
		}

		public override void Tick()
		{
			base.Tick();

			StateNameLabel.Text = Game.Instance.CurrentState?.StateName;
		}
	}
}
