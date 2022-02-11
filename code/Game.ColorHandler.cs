using Sandbox;
using System.Collections.Generic;
using ImposterSyndrome.Systems.Players;
using System.Linq;
using ImposterSyndrome.Systems.States;

namespace ImposterSyndrome
{
	public partial class ImposterSyndrome
	{
		[Net] public Dictionary<Client, Color> AssignedColors { get; set; } = new();

		public static void AssignColorToClient( Client client, int colorIndex )
		{
			if ( Instance is null )
				return;

			// Get colour from index.
			var color = GameConfig.AvailablePlayerColors[colorIndex];

			// Check if this colour is already assigned.
			if ( (Instance.AssignedColors as Dictionary<Client, Color>).ContainsValue( color ) )
				return;

			Instance.AssignedColors.Add( client, color );

			if ( client.Pawn is not ISPlayer player )
				return;

			player.UpdateColor( color );
		}

		public static void AssignRemainingColors()
		{
			if ( Instance is null )
				return;

			var unassignedPlayers = Client.All.Except( Instance.AssignedColors.Keys );

			if ( unassignedPlayers.Count() < 1 )
				return;

			var unassignedColors = GameConfig.AvailablePlayerColors.Except( Instance.AssignedColors.Values ).ToList();

			foreach ( var player in unassignedPlayers )
			{
				var selectedColor = Rand.FromList( unassignedColors );
				unassignedColors.Remove( selectedColor );

				Instance.AssignedColors.Add( player, selectedColor );
			}
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
