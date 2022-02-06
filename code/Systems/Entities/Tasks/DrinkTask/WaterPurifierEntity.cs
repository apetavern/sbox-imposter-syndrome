using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System.ComponentModel;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_task_drink_waterpurifier" )]
	[Hammer.EntityTool( "Cup", "ImposterSyndrome", "The cup entity for the 'Drink' task." )]
	[Hammer.EditorModel( "models/citizen_props/crate01.vmdl" )]
	public class WaterPurifierEntity : BaseUsable
	{
		protected override string ModelPath => "models/citizen_props/crate01.vmdl";

		public override bool IsUsable( ISPlayer user, UseType useType )
		{
			return false;
		}

		public override bool OnUse( ISPlayer user, UseType useType )
		{
			return false;
		}
	}
}
