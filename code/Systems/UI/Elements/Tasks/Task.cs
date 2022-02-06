using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	public class Task : Label
	{
		public Task( BaseTask task )
		{
			StyleSheet.Load( "/Systems/UI/Elements/Tasks/Task.scss" );

			var displayName = task.TaskName;

			if ( task is MultipleTask multiTask )
				displayName += $": {multiTask.ActiveSubTask.TaskName}";

			if ( (Local.Pawn as ISPlayer).IsImposter )
				displayName += "(fake)";

			SetText( displayName );
			BindClass( "completed", () => task.Status == TaskStatus.Complete );
		}
	}
}
