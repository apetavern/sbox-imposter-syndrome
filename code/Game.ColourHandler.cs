using Sandbox;
using System.Collections.Generic;
using ImposterSyndrome.Systems.Players;
using System.Linq;

namespace ImposterSyndrome
{
	public partial class ImposterSyndrome
	{
		[Net] public Dictionary<Client, Color> AssignedColours { get; set; } = new();

		public static void AssignColourToClient( Client client, Color color )
		{
			if ( Instance is null )
				return;

			// Check if this colour is already assigned.
			if ( (Instance.AssignedColours as Dictionary<Client, Color>).ContainsValue( color ) )
				return;

			Instance.AssignedColours.Add( client, color );

			if ( client.Pawn is not ISPlayer player )
				return;

			player.UpdateColor( color );
		}

		public static void AssignRemainingColours()
		{
			if ( Instance is null )
				return;

			var unassignedPlayers = Client.All.Except( Instance.AssignedColours.Keys );

			if ( unassignedPlayers.Count() < 1 )
				return;

			var unassignedColours = GameConfig.AvailablePlayerColors.Except( Instance.AssignedColours.Values ).ToList();

			foreach ( var player in unassignedPlayers )
			{
				var selectedColour = Rand.FromList( unassignedColours );
				unassignedColours.Remove( selectedColour );

				Instance.AssignedColours.Add( player, selectedColour );
			}
		}

		public static void ResetColourAssignment()
		{
			Instance?.AssignedColours.Clear();
		}
	}
}
