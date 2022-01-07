using Sandbox;

namespace Sussies
{
	public partial class Game : Sandbox.Game
	{
		public static Game Instance => Current as Game;

		public Game()
		{

		}

		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new SussiesPlayer();
			client.Pawn = player;

			player.Respawn();
		}
	}
}
