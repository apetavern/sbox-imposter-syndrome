using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using SandboxEditor;
using System;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "A cup entity for the 'Drink' task." )]
	[HammerEntity]
	[EditorModel( "models/citizen_props/coffeemug01.vmdl" )]
	public class CupEntity : MultipleTaskEntity
	{
		public override string UseName => "Pickup";
		protected override string ModelPath => "models/citizen_props/coffeemug01.vmdl";
		protected override Type TargetTaskType { get; set; } = typeof( DrinkTask );
		protected override Type TargetSubTaskType { get; set; } = typeof( FindCup );

	}
}
