using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	[UseTemplate]
	public class TaskProgressPanel : Panel
	{
		public Label ProgressLabel { get; set; }

		public TaskProgressPanel()
		{
			StyleSheet.Load( "/Systems/UI/Elements/Tasks/TaskProgressPanel.scss" );
		}

		public override void Tick()
		{
			base.Tick();

			ProgressLabel.Text = (0).ToString();
		}
	}
}
