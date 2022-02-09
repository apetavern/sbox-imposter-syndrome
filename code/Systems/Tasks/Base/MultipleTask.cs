using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.Tasks
{
	public abstract partial class MultipleTask : BaseTask
	{
		public override string TaskName => "MultipleTask";
		[Net] public int SubTaskProgress { get; set; }
		[Net] public int SubTaskQuantity { get; set; }
		[Net] public SubTask ActiveSubTask { get; set; }
		public List<SubTask> SubTasks { get; set; } = new();

		public MultipleTask()
		{
			SetupSubTasks();
			CountSubTasks();

			AssignNextTask();
		}

		protected virtual void SetupSubTasks() { }

		private void CountSubTasks()
		{
			SubTaskQuantity = SubTasks.Count;
		}

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
			SubTaskProgress++;

			if ( SubTasks.Count > 0 )
			{
				AssignNextTask();

				// Refresh players task list.
				PlayingHudEntity.RefreshTaskList();

				return;
			}

			Status = TaskStatus.Complete;
			OnTaskCompleted();

			// Refresh players task list.
			PlayingHudEntity.RefreshTaskList();
		}

		public override void OnTaskCompleted()
		{
			Log.Info( "Task completed." );
		}
	}
}
