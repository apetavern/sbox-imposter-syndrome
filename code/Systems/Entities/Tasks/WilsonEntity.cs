using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System.ComponentModel;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_task_wilson" )]
	[Hammer.EntityTool( "Wilson", "ImposterSyndrome", "The entity for the 'Find Wilson' task." )]
	[Hammer.EditorModel( "models/citizen_props/beachball.vmdl" )]
	public class WilsonEntity : AnimEntity, IEntityUse
	{
		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/citizen_props/beachball.vmdl" );
			SetupPhysicsFromModel( PhysicsMotionType.Static );
		}

		public bool IsUsable( ISPlayer user, UseType useType )
		{
			if ( useType != UseType.Use )
				return false;

			var task = user.AssignedTasks.OfType<FindWilson>().FirstOrDefault( task => task.Status == TaskStatus.Incomplete );

			return task != null;
		}

		public bool OnUse( ISPlayer user, UseType useType )
		{
			var task = user.AssignedTasks.OfType<FindWilson>().FirstOrDefault( task => task.Status == TaskStatus.Incomplete );
			task.MarkAsCompleted();

			return false;
		}
	}
}
