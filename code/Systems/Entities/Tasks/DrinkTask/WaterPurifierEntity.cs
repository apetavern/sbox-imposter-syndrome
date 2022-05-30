using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using SandboxEditor;
using System;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "The water purifier entity for the 'Drink' task." )]
	[HammerEntity]
	[EditorModel( "models/citizen_props/crate01.vmdl" )]
	public class WaterPurifierEntity : MultipleTaskEntity
	{
		public override string UseName => "Purify";
		protected override string ModelPath => "models/citizen_props/crate01.vmdl";
		protected override Type TargetTaskType { get; set; } = typeof( DrinkTask );
		protected override Type TargetSubTaskType { get; set; } = typeof( PurifyWater );
	}
}
