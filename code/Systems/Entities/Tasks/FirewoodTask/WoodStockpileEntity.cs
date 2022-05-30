using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using SandboxEditor;
using System;
using System.Collections.Generic;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_task_firewood_stockpile" )]
	[HammerEntity]
	[EditorModel( "models/citizen_props/newspaper01.vmdl" )]
	public class WoodStockpileEntity : MultipleTaskEntity
	{
		public override string UseName => "Place";
		protected override string ModelPath => "models/citizen_props/newspaper01.vmdl";
		protected override Type TargetTaskType { get; set; } = typeof( FirewoodTask );
		protected override Type TargetSubTaskType { get; set; } = typeof( PlaceOnWoodStockpile );
		private List<ModelEntity> WoodEntities { get; set; } = new();

		public override bool OnUse( ISPlayer user, UseType useType )
		{
			if ( !IsUsable( user, useType ) )
				return false;

			var woodModel = new ModelEntity( "models/sbox_props/low_wood_fence/low_wood_fence_beam_1_gib2.vmdl", this );
			woodModel.Position = Position;
			WoodEntities.Add( woodModel );

			GetTaskInstance( user )?.MarkAsCompleted();

			return false;
		}

		public override void Reset()
		{
			// Remove all wood
			foreach ( var wood in WoodEntities )
				wood.Delete();

			base.Reset();
		}
	}
}
