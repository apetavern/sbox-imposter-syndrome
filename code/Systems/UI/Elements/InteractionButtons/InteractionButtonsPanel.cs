using ImposterSyndrome.Systems.Entities;
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

			if ( Local.Pawn is not ISPlayer player )
				return;

			AddChild( new InteractionButton( "Use", "waving_hand", () => (Local.Pawn as ISPlayer).LocateUsable() != null, () => UseClick( player ) ) );

			if ( player.IsImposter )
			{
				AddImposterButtons();
				return;
			}

			AddPlayerButtons();
		}

		private void AddImposterButtons()
		{
			AddChild( new InteractionButton( "Kill", "bloodtype", () => false, () => Log.Info( "click kill" ) ) );
		}

		private void AddPlayerButtons()
		{
			AddChild( new InteractionButton( "Report", "report", () => false, () => Log.Info( "click report" ) ) );
		}

		private void UseClick( ISPlayer player )
		{
			ISPlayer.UseNearestEntity();
		}
	}
}
