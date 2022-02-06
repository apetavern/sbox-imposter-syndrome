using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	public class TaskEntity : BaseUsableEntity
	{
		protected virtual Type TargetTaskType { get; set; }

		protected virtual BaseTask GetTaskInstance( ISPlayer user )
		{
			return user.AssignedTasks.FirstOrDefault( task => task.GetType() == TargetTaskType && task.Status == TaskStatus.Incomplete );
		}

		public override bool IsUsable( ISPlayer user, UseType useType )
		{
			if ( useType != UseType.Use )
				return false;

			if ( HasBeenUsedBy( user ) )
				return false;

			return GetTaskInstance( user ) != null;
		}

		public override bool OnUse( ISPlayer user, UseType useType )
		{
			if ( !IsUsable( user, useType ) )
				return false;

			DisableForPlayer( user );
			GetTaskInstance( user )?.MarkAsCompleted();

			return false;
		}
	}
}
