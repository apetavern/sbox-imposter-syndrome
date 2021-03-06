using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.States;
using Sandbox;
using SandboxEditor;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "Where the players will be teleported to at the start of a new playing round." )]
	[HammerEntity]
	[EditorModel( "models/sbox_props/burger_box/burger_box.vmdl" )]
	public class CampfireEntity : BaseUsableEntity
	{
		protected override string ModelPath => "models/sbox_props/burger_box/burger_box.vmdl";
		public static CampfireEntity Instance { get; set; }

		public override void Spawn()
		{
			base.Spawn();

			Instance = this;
		}

		public override bool IsUsable( ISPlayer user, UseType useType )
		{
			if ( useType == UseType.Use && user.LifeState == LifeState.Alive )
				return !user.HasCalledEmergencyMeeting;

			return false;
		}

		public override bool OnUse( ISPlayer user, UseType useType )
		{
			if ( !IsUsable( user, useType ) )
				return false;

			ImposterSyndrome.UpdateState( new VotingState( user ) );
			user.HasCalledEmergencyMeeting = true;

			return false;
		}
	}
}
