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
			RootPanel.AddChild<VotingPanel>();

			_ = new Nametags();
		}

		[ServerCmd]
		public static void Rebuild()
		{
			Instance?.Delete();
			Instance = new PlayerHudEntity();
		}

		[ClientRpc]
		public static void RefreshTaskList()
		{
			TaskListPanel.Instance?.Rebuild();
		}

		[ClientRpc]
		public static void ShowVotingScreen( bool shouldShow )
		{
			VotingPanel.Instance?.Show( shouldShow );
		}
	}
}
