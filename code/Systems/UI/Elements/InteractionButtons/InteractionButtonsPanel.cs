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

			if ( Local.Pawn is not ISBasePlayer basePlayer )
				return;

			AddChild( new InteractionButton( "Report", "report", () => (Local.Pawn as ISBasePlayer).GetNearestUsable( UseType.Report ) != null, () => ISPlayer.UseNearestEntity( UseType.Report ) ) );

			if ( basePlayer is ISPlayer player && player.IsImposter )
			{
				AddImposterButtons( player );
				return;
			}

			AddPlayerButtons( basePlayer );
		}

		private void AddImposterButtons( ISBasePlayer player )
		{
			AddChild( new InteractionButton( "Kill", "bloodtype", () => (Local.Pawn as ISBasePlayer).GetNearestUsable( UseType.Kill ) != null, () => ISPlayer.UseNearestEntity( UseType.Kill ) ) );
		}

		private void AddPlayerButtons( ISBasePlayer player )
		{
			AddChild( new InteractionButton( "Use", "waving_hand", () => (Local.Pawn as ISBasePlayer).GetNearestUsable( UseType.Use ) != null, () => ISPlayer.UseNearestEntity( UseType.Use ) ) );
		}
	}
}
