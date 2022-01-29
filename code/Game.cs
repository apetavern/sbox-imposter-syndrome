using Sandbox;
using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.States;

namespace ImposterSyndrome
{
	public partial class Game : Sandbox.Game
	{
		public static Game Instance => Current as Game;

		public Game()
		{
			UpdateState( new WaitingState() );
		}

		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new ISSpectator();
			player.Respawn();

			client.Pawn = player;
		}
	}
}
