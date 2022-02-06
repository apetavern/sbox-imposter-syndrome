using Sandbox;

namespace ImposterSyndrome.Systems.Tasks
{
	public abstract partial class BaseTask : BaseNetworkable
	{
		public virtual string TaskName { get; set; } = "BaseTask";
		[Net] public TaskStatus Status { get; protected set; }

		public BaseTask FlagAsFake( bool isFakeTask )
		{
			if ( isFakeTask )
				Status = TaskStatus.Fake;

			return this;
		}

		public virtual void MarkAsCompleted()
		{
			Status = TaskStatus.Complete;
			OnTaskCompleted();
		}

		public virtual void OnTaskCompleted()
		{
			Log.Info( "Task completed." );
		}
	}
}
