using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.Tasks
{
	public abstract partial class MultipleTask : BaseTask
	{
		public override string TaskName => "MultipleTask";
		[Net] public SubTask ActiveSubTask { get; set; }
		public List<SubTask> SubTasks { get; set; } = new();

		public MultipleTask()
		{
			SetupSubTasks();
			AssignNextTask();
		}

		protected virtual void SetupSubTasks() { }

		private void AssignNextTask()
		{
			var currTask = SubTasks.FirstOrDefault();
			SubTasks.Remove( currTask );

			if ( currTask is null )
				return;

			currTask.IsActive = true;
			ActiveSubTask = currTask;
		}

		public override void MarkAsCompleted()
		{
			if ( SubTasks.Count > 0 )
			{
				AssignNextTask();

				// Refresh players task list.
				PlayerHudEntity.RefreshTaskList();

				return;
			}

			Status = TaskStatus.Complete;
			OnTaskCompleted();

			// Refresh players task list.
			PlayerHudEntity.RefreshTaskList();
		}

		public override void OnTaskCompleted()
		{
			Log.Info( "Task completed." );
		}
	}
}
