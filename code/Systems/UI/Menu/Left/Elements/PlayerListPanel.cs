using Sandbox.UI;
using Sandbox;
using System.Linq;
using System.Collections.Generic;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class PlayerListPanel : Panel
	{
		public static PlayerListPanel Instance { get; set; }
		private Dictionary<int, PlayerPanel> DisplayedClients { get; set; } = new();

		public PlayerListPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/Elements/PlayerListPanel.scss" );

			Instance = this;
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
			DeleteChildren( true );

			foreach ( var client in Client.All )
			{
				var playerPanel = new PlayerPanel( client.NetworkIdent );

				AddChild( playerPanel );
				DisplayedClients.Add( client.NetworkIdent, playerPanel );
			}
		}
	}
}
