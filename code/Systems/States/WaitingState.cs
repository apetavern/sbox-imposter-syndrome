using ImposterSyndrome.Systems.Players;
using Sandbox;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ImposterSyndrome.Systems.States
{
	public partial class WaitingState : BaseState
	{
		[Net] public override string StateName => "Waiting";
		public override float StateDuration { get; set; } = 30;

		public override void OnStateStarted()
		{
			base.OnStateStarted();

			Game.Instance?.PlayingClients?.Clear();

			foreach ( var player in Client.All.Select( cl => cl.Pawn as ISBasePlayer ) )
				player.UpdatePawn( new ISSpectator() );
		}

		public override void OnStateEnded()
		{
			base.OnStateEnded();

			Game.UpdateState( new PlayingState() );
		}
	}
}
