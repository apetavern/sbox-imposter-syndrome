using ImposterSyndrome.Systems.Players;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

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
			DeleteChildren();

			var useButton = Add.Button( "Use" );
			useButton.BindClass( "enabled", () => (Local.Pawn as ISBasePlayer).LocateUsable() is not null );
			useButton.AddEventListener( "onclick", _ => Log.Info( "Use" ) );

			// Imposter
			if ( isImposter )
			{
				// Add.Button( "Imposter" );
				return;
			}

			// Not imposter.
			// Add.Button( "Not imposter" );
		}
	}
}
