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
			Log.Info( $"🏝️ Round {StateName} started." );
			StateEndTime = Time.Now + StateDuration;
		}

		public virtual void OnStateEnded()
		{
			Log.Info( $"🏝️ Round {StateName} ended." );
		}

		public virtual void OnSecond()
		{
			if ( !Host.IsServer )
				return;

			if ( Game.Instance.CurrentState is PlayingState && !CheckMinimumPlayers() )
			{
				OnStateEnded();
				Game.UpdateState( new GameEndState().SetReason( GameEndReason.NotEnoughPlayers ) );
			}

			if ( Time.Now >= StateEndTime )
				OnStateEnded();
		}

		protected bool CheckMinimumPlayers()
		{
			return Game.Instance.Players.Count >= GameConfig.MinimumPlayers;
		}
	}
}
