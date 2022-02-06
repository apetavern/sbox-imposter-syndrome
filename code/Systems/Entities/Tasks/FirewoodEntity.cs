using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System;
namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_task_firewood" )]
	[Hammer.EntityTool( "Firewood", "ImposterSyndrome", "An entity for the 'Gather Firewood' task." )]
	[Hammer.EditorModel( "models/sbox_props/low_wood_fence/low_wood_fence_beam_1_gib2.vmdl" )]
	public class FirewoodEntity : TaskEntity
	{
		protected override Type TargetTaskType => typeof( GatherFirewood );
		protected override string ModelPath => "models/sbox_props/low_wood_fence/low_wood_fence_beam_1_gib2.vmdl";
	}
}
