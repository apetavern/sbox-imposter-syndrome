using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	public partial class PlayerHudEntity : Sandbox.HudEntity<RootPanel>
	{
		public static PlayerHudEntity Instance { get; set; }

		public PlayerHudEntity()
		{
			Instance = this;

			if ( !IsClient )
				return;

			RootPanel.SetTemplate( "/Systems/UI/PlayerHudEntity.html" );

			_ = new Nametags();
		}

		[ServerCmd]
		public static void Rebuild()
		{
			Instance?.Delete();
			Instance = new PlayerHudEntity();
		}
	}
}
