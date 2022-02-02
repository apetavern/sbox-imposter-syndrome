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

			if ( Local.Pawn is not ISBasePlayer basePlayer )
				return;

			if ( basePlayer is ISPlayer player && player.IsImposter )
			{
				Add.Label( "Kill people" );
				Add.Label( "Fake Tasks:" );
			}

			foreach ( var task in basePlayer.AssignedTasks )
				AddChild( new Task( task ) );
		}
	}
}
