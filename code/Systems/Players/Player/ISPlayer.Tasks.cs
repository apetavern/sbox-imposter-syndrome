using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer
	{
		[Net] public List<BaseTask> AssignedTasks { get; set; }

		public float GetTotalTaskProgress()
		{
			float totalAmount = AssignedTasks.Count();

			if ( totalAmount <= 0 )
				totalAmount = 1;

			float completedAmount = AssignedTasks.Where( task => task.Status == TaskStatus.Complete ).Count();

			return (completedAmount / totalAmount) * 100;
		}
	}
}
