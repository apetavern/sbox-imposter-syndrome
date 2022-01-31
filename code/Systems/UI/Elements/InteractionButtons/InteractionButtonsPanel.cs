using ImposterSyndrome.Systems.Players;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Linq;

namespace ImposterSyndrome.Systems.UI
{
	[UseTemplate]
	public class InteractionButtonsPanel : Panel
	{
		public static InteractionButtonsPanel Instance { get; set; }

		public InteractionButtonsPanel()
		{
			Instance = this;
			StyleSheet.Load( "/Systems/UI/Elements/InteractionButtons/InteractionButtonsPanel.scss" );
		}

		public void RebuildFromImposterStatus( bool isImposter )
		{
			DeleteChildren( true );

			AddChild( new InteractionButton( "Use", () => (Local.Pawn as ISBasePlayer).LocateUsable() != null, () => Log.Info( "click" ) ) );

			// Imposter
			if ( isImposter )
			{
				AddChild( new InteractionButton( "Kill", () => true, () => Log.Info( "click kill" ) ) );
				return;
			}

			// Not imposter.
			AddChild( new InteractionButton( "Report", () => true, () => Log.Info( "click report" ) ) );
		}
	}
}
