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
			if ( !Host.IsServer )
				return;

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
			Players.Remove( cl.Pawn as ISPlayer );

			base.ClientDisconnect( cl, reason );
		}
	}
}
