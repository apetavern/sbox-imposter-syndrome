using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISSpectator : ISBasePlayer
	{
		public override void Respawn()
		{
			base.Respawn();

			// TODO: A noclip walk controller?
			Controller = new ISController();
			Animator = new ISAnimator();
			Camera = new ISCamera();

			UpdateRenderAlpha();
		}
	}
}
