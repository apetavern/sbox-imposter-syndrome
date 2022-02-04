using Sandbox;
using ImposterSyndrome.Systems.States;
using System.Collections.Generic;
using ImposterSyndrome.Systems.Players;

namespace ImposterSyndrome
{
	public partial class ImposterSyndrome
	{
		[Net] public List<ISPlayer> Players { get; set; }
		[Net] public BaseState CurrentState { get; set; }
		private TimeSince TimeSinceLastSecond { get; set; }

		public static void UpdateState( BaseState newState )
		{
			if ( Instance is null )
				return;

			Instance.CurrentState = newState;
			Instance.CurrentState?.OnStateStarted();
		}

		[Event.Tick]
		public void OnSecond()
		{
			if ( TimeSinceLastSecond < 1 )
				return;

			TimeSinceLastSecond = 0;

			if ( !Host.IsServer )
				return;

			Instance.CurrentState?.OnSecond();
		}
	}
}
