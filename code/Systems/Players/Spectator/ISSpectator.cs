using Sandbox;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISSpectator : Sandbox.Player
	{
		public override void Respawn()
		{
			Controller = new NoclipController();

			EnableAllCollisions = false;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}
	}
}
