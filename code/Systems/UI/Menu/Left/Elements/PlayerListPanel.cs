using Sandbox.UI;
using Sandbox;
using System.Collections.Generic;

namespace ImposterSyndrome.Systems.UI.Menu
{
	public class PlayerListPanel : Panel
	{
		private List<Client> DisplayedClients { get; set; } = new();

		public PlayerListPanel()
		{
			StyleSheet.Load( "/Systems/UI/Menu/Left/Elements/PlayerListPanel.scss" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( DisplayedClients.Count == Client.All.Count )
				return;

			// Repopulate list.
			AddClients();
		}

		private void AddClients()
		{
			DisplayedClients.Clear();
			DeleteChildren( true );

			foreach ( var client in Client.All )
			{
				var playerPanel = new PlayerPanel( client.Name );

				AddChild( playerPanel );
				DisplayedClients.Add( client );
			}
		}
	}
}
