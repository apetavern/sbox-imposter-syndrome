using ImposterSyndrome.Systems.Tasks;
using Sandbox.Internal;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	public class Task : Label
	{
		public Task( BaseTask task )
		{
			StyleSheet.Load( "/Systems/UI/Elements/Tasks/Task.scss" );

			SetText( task.GetTaskName() );
			BindClass( "completed", () => task.Status == TaskStatus.Complete );
		}
	}
}
