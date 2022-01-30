using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class TaskListPanel : Panel
	{
		public TaskListPanel()
		{
			StyleSheet.Load( "/Systems/UI/Elements/Tasks/TaskListPanel.scss" );

			Add.Label( "Task 1" );
			Add.Label( "Task 2" );
			Add.Label( "Task 3" );
			Add.Label( "Task 4" );
		}

		public override void Tick()
		{
			base.Tick();
		}
	}
}
