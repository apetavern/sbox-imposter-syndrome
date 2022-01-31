using Sandbox;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_task_wilson" )]
	[Hammer.EntityTool( "Wilson", "ImposterSyndrome", "The entity for the 'Find Wilson' task." )]
	[Hammer.EditorModel( "models/citizen_props/beachball.vmdl" )]
	public class WilsonEntity : AnimEntity, IUse
	{
		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/citizen_props/beachball.vmdl" );
			SetupPhysicsFromModel( PhysicsMotionType.Static );
		}

		public bool OnUse( Entity user )
		{
			return false;
		}

		public bool IsUsable( Entity user )
		{
			return true;
		}
	}
}
