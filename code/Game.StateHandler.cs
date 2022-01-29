using Sandbox;
using ImposterSyndrome.Systems.States;

namespace ImposterSyndrome
{
	public partial class Game : Sandbox.Game
	{
		[Net] public BaseState CurrentState { get; set; }

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
	}
}
