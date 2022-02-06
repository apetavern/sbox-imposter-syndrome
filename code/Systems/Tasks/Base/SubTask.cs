using Sandbox;

namespace ImposterSyndrome.Systems.Tasks
{
	public abstract partial class SubTask : BaseTask
	{
		[Net] public bool IsActive { get; set; }
		public override string TaskName => "SubTask";
	}
}
