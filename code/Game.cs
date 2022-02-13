using Sandbox;
using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.States;
using ImposterSyndrome.Systems.PostProcessing;
using ImposterSyndrome.Systems.UI;

namespace ImposterSyndrome
{
	public partial class ImposterSyndrome : Game
	{
		public static ImposterSyndrome Instance => Current as ImposterSyndrome;

		public ImposterSyndrome()
		{
			if ( Host.IsClient )
			{
				PostEffects.Setup();
				return;
			}

			_ = new PlayingHudEntity();
			_ = new GameConfig();

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
