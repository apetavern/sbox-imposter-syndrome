using Sandbox;
using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.States;
using ImposterSyndrome.Systems.UI;
using ImposterSyndrome.Systems.PostProcessing;
using ImposterSyndrome.Systems.Misc;

namespace ImposterSyndrome
{
	public partial class ImposterSyndrome : Game
	{
		public static ImposterSyndrome Instance => Current as ImposterSyndrome;

		public ImposterSyndrome()
		{
			if ( Host.IsClient )
			{
				Fog.Initialize( new BBox( Vector3.One * -5000f, Vector3.One * 5000f ) );
				PostEffects.Setup();
			}

			if ( !Host.IsServer )
				return;

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
			Players.Remove( cl.Pawn as ISPlayer );

			base.ClientDisconnect( cl, reason );
		}
	}
}
