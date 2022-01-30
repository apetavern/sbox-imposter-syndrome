using ImposterSyndrome.Systems.Tasks;
using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Collections.Generic;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer : ISBasePlayer
	{
		[Net] public List<BaseTask> AssignedTasks { get; set; }
		[Net, Local] public bool IsImposter { get; set; }

		public override void Respawn()
		{
			base.Respawn();
		}
	}
}
