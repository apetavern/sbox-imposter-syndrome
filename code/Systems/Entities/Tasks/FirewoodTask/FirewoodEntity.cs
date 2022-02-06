using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_task_firewood" )]
	[Hammer.EntityTool( "Firewood", "ImposterSyndrome", "An entity for the 'Gather Firewood' task." )]
	[Hammer.EditorModel( "models/sbox_props/low_wood_fence/low_wood_fence_beam_1_gib2.vmdl" )]
	public class FirewoodEntity : MultipleTaskEntity
	{
		protected override string ModelPath => "models/sbox_props/low_wood_fence/low_wood_fence_beam_1_gib2.vmdl";
		protected override Type TargetTaskType { get; set; } = typeof( FirewoodTask );
		protected override Type TargetSubTaskType { get; set; } = typeof( GatherFirewood );
	}
}
