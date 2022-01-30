using ImposterSyndrome.Systems.UI;
using Sandbox;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer : ISBasePlayer
	{
		[Net, Local] public bool IsImposter { get; set; }

		public override void Respawn()
		{
			base.Respawn();

			// Let this players HUD know they're imposter.
			PlayerHudEntity.RebuildFromImposterStatus( IsImposter );
		}
	}
}
