using Sandbox.UI;
using Sandbox;
using System.Collections.Generic;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class MenuPlayerListPanel : Panel
	{
		private Dictionary<int, PlayerPanel> DisplayedClients { get; set; } = new();

		public MenuPlayerListPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/Elements/MenuPlayerListPanel.scss" );
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
