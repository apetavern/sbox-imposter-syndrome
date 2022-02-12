using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.UI.Menu
{
	[UseTemplate]
	public class GameConfigPanel : Panel
	{
		public static GameConfigPanel Instance { get; set; }

		public int PlayersPerImposter { get; set; } = 2;
		public int NumberOfTasks { get; set; } = 6;

		public GameConfigPanel()
		{
			Instance = this;
		}
	}
}
