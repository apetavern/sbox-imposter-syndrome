using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_task_firewood_stockpile" )]
	[Hammer.EntityTool( "Wood Stockpile", "ImposterSyndrome", "The wood stock pile for the 'Gather Firewood' task." )]
	[Hammer.EditorModel( "models/citizen_props/newspaper01.vmdl" )]
	public class WoodStockpileEntity : MultipleTaskEntity
	{
		protected override string ModelPath => "models/citizen_props/newspaper01.vmdl";
		protected override Type TargetTaskType { get; set; } = typeof( FirewoodTask );
		protected override Type TargetSubTaskType { get; set; } = typeof( PlaceOnWoodStockpile );

		public override bool OnUse( ISPlayer user, UseType useType )
		{
			if ( !IsUsable( user, useType ) )
				return false;

			var woodModel = new ModelEntity( "models/sbox_props/low_wood_fence/low_wood_fence_beam_1_gib2.vmdl", this );
			woodModel.Position = Position;

			GetTaskInstance( user )?.MarkAsCompleted();

			return false;
		}
	}
}
