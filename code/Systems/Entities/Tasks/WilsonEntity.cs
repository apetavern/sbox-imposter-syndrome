using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_task_wilson" )]
	[Hammer.EntityTool( "Wilson", "ImposterSyndrome", "The entity for the 'Find Wilson' task." )]
	[Hammer.EditorModel( "models/wilson/wilson.vmdl" )]
	public class WilsonEntity : TaskEntity
	{
		public override string UseName => "Pickup";
		protected override Type TargetTaskType => typeof( FindWilson );
		protected override string ModelPath => "models/wilson/wilson.vmdl";
	}
}
