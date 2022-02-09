using ImposterSyndrome.Systems.Players;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;

namespace ImposterSyndrome.Systems.UI
{
	public class TaskListPanel : Panel
	{
		public static TaskListPanel Instance { get; set; }
		private List<Task> Tasks { get; set; } = new();

		public TaskListPanel()
		{
			Instance = this;
			StyleSheet.Load( "/Systems/UI/Gameplay/Elements/Tasks/TaskListPanel.scss" );

			if ( Local.Pawn is not ISPlayer player )
				return;

			if ( player.IsImposter )
			{
				Add.Label( "Kill people" );
			}

			foreach ( var task in player.AssignedTasks )
			{
				var taskPanel = new Task( task );
				Tasks.Add( taskPanel );

				AddChild( taskPanel );
			}
		}

		public void Refresh()
		{
			foreach ( var task in Tasks )
				task.Refresh();
		}
	}
}
