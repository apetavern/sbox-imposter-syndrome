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

			AddChild( new InteractionButton( "Report", "report", () => (Local.Pawn as ISPlayer).GetNearestUsable( UseType.Report ) != null, () => ISPlayer.UseNearestEntity( UseType.Report ) ) );

			if ( player.IsImposter )
			{
				AddImposterButtons( player );
				return;
			}

			AddPlayerButtons( player );
		}

		private void AddImposterButtons( ISPlayer player )
		{
			AddChild( new InteractionButton( "Kill", "bloodtype", () => (Local.Pawn as ISPlayer).GetNearestUsable( UseType.Kill ) != null, () => ISPlayer.UseNearestEntity( UseType.Kill ) ) );
		}

		private void AddPlayerButtons( ISPlayer player )
		{
			AddChild( new InteractionButton( "Use", "waving_hand", () => (Local.Pawn as ISPlayer).GetNearestUsable( UseType.Use ) != null, () => ISPlayer.UseNearestEntity( UseType.Use ) ) );
		}
	}
}
