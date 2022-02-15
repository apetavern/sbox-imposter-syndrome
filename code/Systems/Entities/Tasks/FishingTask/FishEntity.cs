using Sandbox;

namespace ImposterSyndrome.Systems.Entities
{
	public partial class FishEntity : AnimEntity
	{
		private FishShoalEntity ParentShoal { get; set; }
		public bool IsHooked { get; set; }

		public FishEntity AddToShoal( FishShoalEntity shoal )
		{
			SetModel( "models/citizen_props/sodacan01.vmdl" );
			ParentShoal = shoal;

			Position = shoal.Position + Vector3.Random * 10;
			return this;
		}

		[Event.Tick.Server]
		public void Tick()
		{
			if ( ParentShoal is null || !ParentShoal.IsValid )
				Delete();

			// Move
			// Rotate
			// Lookout for bait in the water, rotate towards and nibble if found
		}

		private void HookTo()
		{

		}
	}
}
