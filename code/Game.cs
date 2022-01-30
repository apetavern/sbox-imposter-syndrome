using Sandbox;
using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.States;
using ImposterSyndrome.Systems.UI;

namespace ImposterSyndrome
{
	public partial class Game : Sandbox.Game
	{
		public static Game Instance => Current as Game;

		public Game()
		{
			_ = new PlayerHudEntity();

			UpdateState( new WaitingState() );
		}

		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new ISSpectator();
			player.Respawn();

			client.Pawn = player;
		}

		public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason )
		{
			base.ClientDisconnect( cl, reason );

			PlayingClients.Remove( cl );
		}
	}
}
