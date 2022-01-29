using Sandbox;

namespace ImposterSyndrome.Systems.Tasks
{
	public abstract partial class BaseTask : BaseNetworkable
	{
		[Net] public virtual string TaskName => "BaseTask";
		[Net] public TaskStatus Status { get; set; }

		public virtual void OnTaskCompleted() { }
	}
}
