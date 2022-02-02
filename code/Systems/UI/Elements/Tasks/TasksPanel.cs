using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	public class TasksPanel : Panel
	{
		public TasksPanel()
		{
			StyleSheet.Load( "/Systems/UI/Elements/Tasks/TasksPanel.scss" );

			AddChild<TaskProgressPanel>();
			AddChild<TaskListPanel>();
		}
	}
}
