using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_task_wilson" )]
	[Hammer.EntityTool( "Wilson", "ImposterSyndrome", "The entity for the 'Find Wilson' task." )]
	[Hammer.EditorModel( "models/citizen_props/beachball.vmdl" )]
	public class WilsonEntity : TaskEntity
	{
		protected override Type TargetTaskType => typeof( FindWilson );
		protected override string ModelPath => "models/citizen_props/beachball.vmdl";
	}
}
