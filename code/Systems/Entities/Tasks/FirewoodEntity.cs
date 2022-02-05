using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System.ComponentModel;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_task_firewood" )]
	[Hammer.EntityTool( "Firewood", "ImposterSyndrome", "An entity for the 'Gather Firewood' task." )]
	[Hammer.EditorModel( "models/sbox_props/low_wood_fence/low_wood_fence_beam_1_gib2.vmdl" )]
	public class FirewoodEntity : AnimEntity, IEntityUse
	{
		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/sbox_props/low_wood_fence/low_wood_fence_beam_1_gib2.vmdl" );
			SetupPhysicsFromModel( PhysicsMotionType.Static );
		}

		public bool IsUsable( ISPlayer user, UseType useType )
		{
			if ( useType != UseType.Use )
				return false;

			var task = user.AssignedTasks.OfType<GatherFirewood>().FirstOrDefault( task => task.Status == TaskStatus.Incomplete );

			return task != null;
		}

		public bool OnUse( ISPlayer user, UseType useType )
		{
			var task = user.AssignedTasks.OfType<GatherFirewood>().FirstOrDefault( task => task.Status == TaskStatus.Incomplete );
			task.MarkAsCompleted();

			return false;
		}
	}
}
