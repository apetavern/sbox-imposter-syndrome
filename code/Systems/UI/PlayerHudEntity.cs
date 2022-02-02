using Sandbox;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	[Library]
	public partial class PlayerHudEntity : HudEntity<RootPanel>
	{
		public static PlayerHudEntity Instance { get; set; }

		public PlayerHudEntity()
		{
			Instance = this;

			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( "/Systems/UI/PlayerHudEntity.scss" );

			RootPanel.AddChild<DevMenu>();
			RootPanel.AddChild<ChatBox>();
			RootPanel.AddChild<GameInfoPanel>();
			RootPanel.AddChild<TasksPanel>();
			RootPanel.AddChild<InteractionButtonsPanel>();

			_ = new Nametags();
		}

		[ServerCmd]
		public static void Destroy()
		{
			Instance?.Delete();
		}

		[ServerCmd]
		public static void Rebuild()
		{
			Instance?.Delete();
			Instance = new PlayerHudEntity();
		}
	}
}
