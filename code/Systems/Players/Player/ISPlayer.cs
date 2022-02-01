using ImposterSyndrome.Systems.UI;
using Sandbox;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer : ISBasePlayer
	{
		[Net, Local, Change] public bool IsImposter { get; set; }

		public override void OnKilled()
		{
			base.OnKilled();

			// Log this as a new death.
		}

		public override void Simulate( Client cl )
		{
			if ( LifeState == LifeState.Dead )
				return;

			var controller = GetActiveController();
			controller?.Simulate( cl, this, GetActiveAnimator() );
		}

		public override void Respawn()
		{
			base.Respawn();
		}

		public void OnIsImposterChanged()
		{
			PlayerHudEntity.Rebuild();
		}
	}
}
