using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using SandboxEditor;
using System;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "The entity for the 'Find Wilson' task." )]
	[HammerEntity]
	[EditorModel( "models/wilson/wilson.vmdl" )]
	public class WilsonEntity : TaskEntity
	{
		public override string UseName => "Pickup";
		protected override Type TargetTaskType => typeof( FindWilson );
		protected override string ModelPath => "models/wilson/wilson.vmdl";

		public override void Spawn()
		{
			base.Spawn();

			if ( Rand.Int( 5 ) == 5 )
				SetMaterialGroup( 1 );
		}
	}
}
