using Sandbox.UI;
using Sandbox;
using System.Collections.Generic;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class MenuPlayerListPanel : Panel
	{
		private Dictionary<int, PlayerPanel> DisplayedClients { get; set; } = new();
		private Panel ListPanel { get; set; }

		public MenuPlayerListPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/Elements/MenuPlayerListPanel.scss" );

			Add.Label( "Current Players", "title" );
			Add.Label( $"Minimum players: {GameConfig.MinimumPlayers}", "subtitle" );

			ListPanel = Add.Panel( "playerlist" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( DisplayedClients.Count == Client.All.Count )
				return;

			// Repopulate list.
			RepopulateClients();
		}

		public void RepopulateClients()
		{
			DisplayedClients.Clear();
			ListPanel.DeleteChildren( true );

			foreach ( var client in Client.All )
			{
				var playerPanel = new PlayerPanel( client.NetworkIdent );

				ListPanel.AddChild( playerPanel );
				DisplayedClients.Add( client.NetworkIdent, playerPanel );
			}
		}
	}
}
