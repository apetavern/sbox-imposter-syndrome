using Sandbox;
using Sandbox.UI;
using ImposterSyndrome.Systems.UI.Menu;
using ImposterSyndrome.Systems.UI.GameEnd;
using ImposterSyndrome.Systems.UI.Dev;

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

			RootPanel.StyleSheet.Load( "/Systems/UI/PlayingHudEntity.scss" );

			RootPanel.AddChild<MenuPanel>();

			RootPanel.AddChild<DevMenu>();
			RootPanel.AddChild<ChatBox>();
			RootPanel.AddChild<GameInfoPanel>();
			RootPanel.AddChild<TasksPanel>();
			RootPanel.AddChild<InteractionButtonsPanel>();
			RootPanel.AddChild<VotingPanel>();

			RootPanel.AddChild<GameEndPanel>();

			_ = new Nametags();
		}

		[ConCmd.Server]
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
		public static void RefreshConfigPanel()
		{
			GameConfigPanel.Instance?.Refresh();
		}

		[ClientRpc]
		public static void UpdateMenuColorUsage( int colorIndex, bool isUsable )
		{
			ColorSelectionPanel.Instance.MarkColorUsable( colorIndex, isUsable );
		}

		[ClientRpc]
		public static void UpdateBackpackColor( int colorIndex )
		{
			MenuScene.Instance?.UpdateBackpackColor( colorIndex );
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
