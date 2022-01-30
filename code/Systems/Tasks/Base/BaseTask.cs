using Sandbox;

namespace ImposterSyndrome.Systems.Tasks
{
	public abstract partial class BaseTask : BaseNetworkable
	{
		protected virtual string TaskName { get; set; } = "BaseTask";
		[Net] public TaskStatus Status { get; set; }

		public BaseTask FlagAsFake( bool isFakeTask )
		{
			if ( isFakeTask )
				Status = TaskStatus.Fake;

			return this;
		}

		public string GetTaskName()
		{
			return Status == TaskStatus.Fake ? $"{TaskName} (fake)" : TaskName;
		}

		public virtual void OnTaskCompleted() { }
	}
}
