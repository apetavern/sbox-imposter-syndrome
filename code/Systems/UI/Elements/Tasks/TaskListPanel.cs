using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;

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

			if ( isImposter )
			{
				Add.Label( "Kill people" );
				Add.Label( "Fake Tasks:" );
			}

			foreach ( var task in (Local.Pawn as ISPlayer).AssignedTasks )
				AddChild( new Task( task ) );
		}
	}
}
