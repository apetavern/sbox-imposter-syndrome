using Sandbox;

namespace ImposterSyndrome.Systems.States
{
	public abstract partial class BaseState : BaseNetworkable
	{
		[Net] public virtual string StateName => "Default";
		public virtual float StateDuration { get; set; }
		[Net] protected float StateEndTime { get; set; }

		public virtual void OnStateStarted()
		{
			StateEndTime = Time.Now + StateDuration;
		}

		public virtual void OnStateEnded() { }
	}
}
