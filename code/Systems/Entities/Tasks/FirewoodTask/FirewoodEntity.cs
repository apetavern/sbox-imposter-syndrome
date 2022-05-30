using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using SandboxEditor;
using System;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "Wood entity for the 'Gather Firewood' task." )]
	[HammerEntity]
	[EditorModel( "models/sbox_props/low_wood_fence/low_wood_fence_beam_1_gib2.vmdl" )]
	public class FirewoodEntity : MultipleTaskEntity
	{
		public override string UseName => "Pickup";
		protected override string ModelPath => "models/sbox_props/low_wood_fence/low_wood_fence_beam_1_gib2.vmdl";
		protected override Type TargetTaskType { get; set; } = typeof( FirewoodTask );
		protected override Type TargetSubTaskType { get; set; } = typeof( GatherFirewood );
	}
}
