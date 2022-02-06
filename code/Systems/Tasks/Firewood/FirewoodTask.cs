namespace ImposterSyndrome.Systems.Tasks
{
	public partial class FirewoodTask : MultipleTask
	{
		public override string TaskName => "Firewood";

		protected override void SetupSubTasks()
		{
			SubTasks = new()
			{
				new GatherFirewood(),
				new GatherFirewood(),
				new GatherFirewood()
			};
		}
	}
}
