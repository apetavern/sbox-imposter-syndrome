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

			if ( player.IsImposter )
			{
				AddImposterButtons( player );
				return;
			}

			AddPlayerButtons( player );
		}

		private void AddImposterButtons( ISPlayer player )
		{
			AddChild( new InteractionButton( "Kill", "bloodtype", () => false, () => Log.Info( "click kill" ) ) );
		}

		private void AddPlayerButtons( ISPlayer player )
		{
			AddChild( new InteractionButton( "Use", "waving_hand", () => (Local.Pawn as ISPlayer).LocateUsable() != null, () => UseClick( player ) ) );
			AddChild( new InteractionButton( "Report", "report", () => false, () => Log.Info( "click report" ) ) );
		}

		private void UseClick( ISPlayer player )
		{
			ISPlayer.UseNearestEntity();
		}
	}
}
