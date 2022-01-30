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

			// Imposter
			if ( isImposter )
			{
				Add.Label( "Imposter task 1" );
				Add.Label( "Imposter task 2" );
				Add.Label( "Imposter task 3" );
				Add.Label( "Imposter task 4" );
				return;
			}

			// Not imposter.
			Add.Label( "Task 1" );
			Add.Label( "Task 2" );
			Add.Label( "Task 3" );
			Add.Label( "Task 4" );
		}
	}
}
