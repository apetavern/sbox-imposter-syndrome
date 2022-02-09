using ImposterSyndrome.Systems.Players;
using Sandbox;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	public class InteractionButtonsPanel : Panel
	{
		public static InteractionButtonsPanel Instance { get; set; }

		public InteractionButtonsPanel()
		{
			Instance = this;
			StyleSheet.Load( "/Systems/UI/Gameplay/Elements/InteractionButtons/InteractionButtonsPanel.scss" );

			if ( Local.Pawn is not ISPlayer player )
				return;

			AddChild( new InteractionButton( "Report", "report", () => player.GetNearestUsable( UseType.Report ) != null, () => ISPlayer.UseNearestEntity( UseType.Report ) ) );

			if ( player.IsImposter )
			{
				AddImposterButtons( player );
				return;
			}

			AddPlayerButtons( player );
		}

		private void AddImposterButtons( ISPlayer player )
		{
			AddChild( new InteractionButton( "Kill", "bloodtype", () => player.GetNearestUsable( UseType.Kill ) != null, () => ISPlayer.UseNearestEntity( UseType.Kill ) ) );
		}

		private void AddPlayerButtons( ISPlayer player )
		{
			AddChild( new DynamicInteractionButton( "waving_hand", () => ISPlayer.UseNearestEntity( UseType.Use ) ) );
		}
	}
}
