using Sandbox;
using ImposterSyndrome.Systems.States;
using System.Collections.Generic;
using ImposterSyndrome.Systems.Players;

namespace ImposterSyndrome
{
	public partial class Game : Sandbox.Game
	{
		[Net] public List<Client> PlayingClients { get; set; }
		[Net] public BaseState CurrentState { get; set; }
		private TimeSince TimeSinceLastSecond { get; set; }

		public static void UpdateState( BaseState newState )
		{
			if ( Instance is null )
				return;

			Instance.CurrentState = newState;
			Instance.CurrentState?.OnStateStarted();
		}

		[ServerCmd( "is_nextstate" )]
		public static void EndCurrentState()
		{
			Game.Instance?.CurrentState?.OnStateEnded();
		}

		[Event.Tick]
		public void OnSecond()
		{
			if ( TimeSinceLastSecond < 1 )
				return;

			Instance.CurrentState?.OnSecond();
			TimeSinceLastSecond = 0;
		}
	}
}
