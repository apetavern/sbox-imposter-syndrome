using Sandbox;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	public partial class PlayerHudEntity : Sandbox.HudEntity<RootPanel>
	{
		public static PlayerHudEntity Instance { get; set; }

		public PlayerHudEntity()
		{
			if ( !IsClient )
				return;

			Instance = this;
			RootPanel.SetTemplate( "/Systems/UI/PlayerHudEntity.html" );

			_ = new Nametags();
		}

		[ClientRpc]
		public static void RebuildFromImposterStatus( bool isImposter )
		{
			InteractionButtonsPanel.Instance?.RebuildFromImposterStatus( isImposter );
			TaskListPanel.Instance?.RebuildFromImposterStatus( isImposter );
		}
	}
}
