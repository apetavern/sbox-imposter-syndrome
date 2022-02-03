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

			Rebuild();
		}

		public void Rebuild()
		{
			DeleteChildren();

			if ( Local.Pawn is not ISPlayer player )
				return;

			if ( player.IsImposter )
			{
				Add.Label( "Kill people" );
				Add.Label( "Fake Tasks:" );
			}

			foreach ( var task in player.AssignedTasks )
				AddChild( new Task( task ) );
		}
	}
}
