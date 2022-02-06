using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System;
using System.ComponentModel;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_task_drink_cup" )]
	[Hammer.EntityTool( "Cup", "ImposterSyndrome", "The cup entity for the 'Drink' task." )]
	[Hammer.EditorModel( "models/citizen_props/coffeemug01.vmdl" )]
	public class CupEntity : MultipleTaskEntity
	{
		protected override string ModelPath => "models/citizen_props/coffeemug01.vmdl";
		protected override Type ParentMultipleTaskType { get; set; } = typeof( DrinkTask );
		protected override Type TargetSubTaskType { get; set; } = typeof( FindCup );

	}
}
