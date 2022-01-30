using ImposterSyndrome.Systems.Players;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	[UseTemplate]
	public class PlayerRolePanel : Panel
	{
		public Label RoleLabel { get; set; }

		public override void Tick()
		{
			base.Tick();

			RoleLabel.Text = Local.Pawn is ISPlayer player ? (player.IsImposter ? "Imposter" : "Player") : "Spectator";
		}
	}
}
