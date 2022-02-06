using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using System;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	public class MultipleTaskEntity : TaskEntity
	{
		protected virtual Type TargetSubTaskType { get; set; }

		protected override BaseTask GetTaskInstance( ISPlayer user )
		{
			return user.AssignedTasks
				.FirstOrDefault(
					task => task.GetType() == TargetTaskType
					&& (task as MultipleTask).ActiveSubTask.GetType() == TargetSubTaskType
				);
		}
	}
}
