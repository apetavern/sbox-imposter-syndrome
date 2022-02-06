using Sandbox;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_gameplay_campfire" )]
	[Hammer.EntityTool( "Campfire", "ImposterSyndrome", "Where the players will be teleported to at the start of a new playing round." )]
	[Hammer.EditorModel( "models/sbox_props/burger_box/burger_box.vmdl" )]
	public class CampfireEntity : BaseUsable
	{
		protected override string ModelPath => "models/sbox_props/burger_box/burger_box.vmdl";
		public static CampfireEntity Instance { get; set; }

		public override void Spawn()
		{
			base.Spawn();

			Instance = this;
		}
	}
}
