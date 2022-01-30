using ImposterSyndrome.Systems.Players;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class TaskListPanel : Panel
	{
		public static TaskListPanel Instance { get; set; }

		public TaskListPanel()
		{
			Instance = this;
			StyleSheet.Load( "/Systems/UI/Elements/Tasks/TaskListPanel.scss" );
		}

		public void RebuildFromImposterStatus( bool isImposter )
		{
			DeleteChildren();

			var player = (Local.Pawn as ISPlayer);

			// Imposter
			if ( isImposter )
			{
				Add.Label( "Kill people" );
				Add.Label( "Fake Tasks:" );
			}

			foreach ( var task in player.AssignedTasks )
			{
				Add.Label( task.GetTaskName() );
			}
		}
	}
}
