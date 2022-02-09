using Sandbox;

namespace ImposterSyndrome.Systems.States
{
	public abstract partial class BaseState : BaseNetworkable
	{
		[Net] public virtual string StateName => "Default";
		public virtual float StateDuration { get; set; }
		[Net] public float StateEndTime { get; set; }

		public virtual void OnStateStarted()
		{
			StateEndTime = Time.Now + StateDuration;
		}

		public virtual void OnStateEnded() { }

		public virtual void OnSecond()
		{
			if ( !Host.IsServer )
				return;

			if ( ImposterSyndrome.Instance.CurrentState is PlayingState && !CheckMinimumPlayers() )
			{
				OnStateEnded();
				ImposterSyndrome.UpdateState( new GameEndState().SetReason( GameEndReason.NotEnoughPlayers ) );
			}

			if ( Time.Now >= StateEndTime )
				OnStateEnded();
		}

		protected bool CheckMinimumPlayers()
		{
			return ImposterSyndrome.Instance.Players.Count >= GameConfig.MinimumPlayers;
		}

		[ServerCmd]
		public static void UpdateState( string statename )
		{
			switch ( statename )
			{
				case "waiting":
					// Meet min player requirement.
					if ( Client.All.Count < GameConfig.MinimumPlayers )
					{
						var difference = GameConfig.MinimumPlayers - Client.All.Count;

						for ( int i = 0; i < difference; i++ )
						{
							new Bot();
						}
					}
					ImposterSyndrome.UpdateState( new WaitingState() );
					break;
				case "end":
					ImposterSyndrome.UpdateState( new GameEndState() );
					break;
			}
		}
	}
}
