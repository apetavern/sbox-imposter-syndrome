using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer : ISBasePlayer
	{
		[Net, Local, Change] public bool IsImposter { get; set; }
		private TimeSince TimeSinceKilled { get; set; }

		public override void OnKilled()
		{
			base.OnKilled();

			PhysicsClear();

			LifeState = LifeState.Dead;

			UpdateRenderAlpha();

			TimeSinceKilled = 0;
		}

		public override void Simulate( Client cl )
		{
			var controller = GetActiveController();
			controller?.Simulate( cl, this, GetActiveAnimator() );
		}

		public void OnIsImposterChanged()
		{
			PlayerHudEntity.Rebuild();
		}
	}
}
