using Sandbox;
using System.Collections.Generic;
using System.Linq;
using ImposterSyndrome.Systems.States;
using ImposterSyndrome.Systems.UI;

namespace ImposterSyndrome
{
	public partial class ImposterSyndrome
	{
		[Net] public Dictionary<Client, int> AssignedColors { get; set; }

		public static void AssignColorToClient( Client client, int colorIndex )
		{
			if ( Instance is null )
				return;

			// Check if this colour is already assigned.
			if ( Instance.AssignedColors.Any( x => x.Value == colorIndex ) )
				return;

			if ( Instance.AssignedColors.Any( x => x.Key == client ) )
				ClearPlayerSelection( client );

			Instance.AssignedColors.Add( client, colorIndex );

			PlayingHudEntity.UpdateMenuColorUsage( colorIndex, false, client.NetworkIdent );
		}

		public static void ClearPlayerSelection( Client client )
		{
			var entry = Instance.AssignedColors.FirstOrDefault( x => x.Key == client );

			PlayingHudEntity.UpdateMenuColorUsage( entry.Value, true, client.NetworkIdent );
			Instance.AssignedColors.Remove( entry.Key );
		}

		public static void AssignRemainingColors()
		{
			if ( Instance is null )
				return;

			var unassignedPlayers = Client.All.Except( Instance.AssignedColors.Keys );

			if ( unassignedPlayers.Count() < 1 )
				return;
		}

		[ServerCmd]
		public static void SelectColor( int indexOfColor )
		{
			if ( Host.IsServer )
				return;

			if ( ImposterSyndrome.Instance.CurrentState is not WaitingState )
				return;

			var client = ConsoleSystem.Caller;
			var color = GameConfig.AvailablePlayerColors[indexOfColor];

			Log.Info( $"Player {client.Name} selected color {color}" );

			AssignColorToClient( client, indexOfColor );
		}

		public static void ResetColorAssignment()
		{
			Instance?.AssignedColors.Clear();
		}
	}
}
