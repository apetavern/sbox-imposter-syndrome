using ImposterSyndrome.Systems.Players;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	[UseTemplate]
	public class TaskProgressPanel : Panel
	{
		public Label ProgressLabel { get; set; }

		public override void Tick()
		{
			ProgressLabel.Text = ISPlayer.GetAllPlayersTaskProgress().ToString();
		}
	}
}
