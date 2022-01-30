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

			// Imposter
			if ( isImposter )
			{
				Add.Button( "Imposter" );
				return;
			}

			// Not imposter.
			Add.Button( "Not imposter" );
		}
	}
}
