using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System;
using System.ComponentModel;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	public class MultipleTaskEntity : BaseUsableEntity
	{
		protected virtual Type ParentMultipleTaskType { get; set; }
		protected virtual Type TargetSubTaskType { get; set; }

		protected override BaseTask GetTaskInstance( ISPlayer user )
		{
			return user.AssignedTasks
				.FirstOrDefault(
					task => task.GetType() == ParentMultipleTaskType
					&& (task as MultipleTask).ActiveSubTask.GetType() == TargetSubTaskType
				);
		}

		public override bool IsUsable( ISPlayer user, UseType useType )
		{
			if ( useType != UseType.Use )
				return false;

			return GetTaskInstance( user ) != null;
		}

		public override bool OnUse( ISPlayer user, UseType useType )
		{
			if ( !IsUsable( user, useType ) )
				return false;

			GetTaskInstance( user )?.MarkAsCompleted();

			return false;
		}
	}
}
