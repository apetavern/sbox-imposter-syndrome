﻿using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	[UseTemplate]
	public class PlayerListPanel : Panel
	{
		public Label ActivePlayerCountLabel { get; set; }

		public override void Tick()
		{
			base.Tick();

			if ( Game.Instance.Players is null )
				return;

			ActivePlayerCountLabel.Text = Game.Instance.Players.Count.ToString();
		}
	}
}