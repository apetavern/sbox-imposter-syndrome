using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.Tasks
{
	public partial class DrinkTask : MultipleTask
	{
		public override string TaskName => "Drink";

		protected override void SetupSubTasks()
		{
			SubTasks = new()
			{
				new FindCup()
			};
		}
	}
}
