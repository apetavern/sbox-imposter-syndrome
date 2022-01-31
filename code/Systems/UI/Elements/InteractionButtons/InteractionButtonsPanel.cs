using ImposterSyndrome.Systems.Players;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Linq;

namespace ImposterSyndrome.Systems.UI
{
	public class InteractionButtonsPanel : Panel
	{
		public static InteractionButtonsPanel Instance { get; set; }

		public InteractionButtonsPanel()
		{
			Instance = this;
			StyleSheet.Load( "/Systems/UI/Elements/InteractionButtons/InteractionButtonsPanel.scss" );

			AddChild( new InteractionButton( "Use", () => (Local.Pawn as ISBasePlayer).LocateUsable() != null, () => Log.Info( "click" ) ) );

			if ( Local.Pawn is not ISPlayer player )
				return;

			if ( player.IsImposter )
			{
				AddImposterButtons();
				return;
			}

			AddPlayerButtons();
		}

		private void AddImposterButtons()
		{
			AddChild( new InteractionButton( "Kill", () => false, () => Log.Info( "click kill" ) ) );
		}

		private void AddPlayerButtons()
		{
			AddChild( new InteractionButton( "Report", () => false, () => Log.Info( "click report" ) ) );
		}
	}
}
