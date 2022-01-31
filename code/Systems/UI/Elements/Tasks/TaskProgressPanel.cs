using ImposterSyndrome.Systems.Players;
using Sandbox;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	[UseTemplate]
	public class TaskProgressPanel : Panel
	{
		public Label ProgressLabel { get; set; }

		public override void Tick()
		{
			ProgressLabel.Text = (Local.Pawn as ISPlayer).GetTotalTaskProgress().ToString();
		}
	}
}
