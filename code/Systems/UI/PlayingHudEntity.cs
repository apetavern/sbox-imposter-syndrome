using Sandbox;
using Sandbox.UI;

namespace ImposterSyndrome.Systems.UI
{
	public partial class PlayingHudEntity : HudEntity<RootPanel>
	{
		public static PlayingHudEntity Instance { get; set; }

		public PlayingHudEntity()
		{
			Instance = this;

			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( "/Systems/UI/PlayerHudEntity.scss" );

			RootPanel.AddChild<MenuPanel>();

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
			Instance = new PlayingHudEntity();
		}

		[ClientRpc]
		public static void RefreshTaskList()
		{
			TaskListPanel.Instance?.Refresh();
		}

		[ClientRpc]
		public static void ShowVotingScreen( bool shouldShow, int calledByNetIdent = -1 )
		{
			VotingPanel.Instance?.Show( shouldShow, calledByNetIdent );
		}

		[ClientRpc]
		public static void ReceivePlayerVote( int voteToPlayer, int voteFromPlayer )
		{
			VotingPanel.Instance?.UpdateVoteFromPlayer( voteFromPlayer, voteToPlayer );
		}
	}
}
