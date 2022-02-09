using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	public class Task : Label
	{
		private BaseTask HeldTask { get; set; }

		public Task( BaseTask task )
		{
			StyleSheet.Load( "/Systems/UI/Gameplay/Elements/Tasks/Task.scss" );

			HeldTask = task;

			Refresh();

			BindClass( "completed", () => task.Status == TaskStatus.Complete );
		}

		public void Refresh()
		{
			var displayName = HeldTask.TaskName;

			if ( HeldTask is MultipleTask multiTask )
				displayName += $": {multiTask.ActiveSubTask.TaskName} ({multiTask.SubTaskProgress}/{multiTask.SubTaskQuantity})";

			if ( (Local.Pawn as ISPlayer).IsImposter )
				displayName += " (fake)";

			SetText( displayName );
		}
	}
}
