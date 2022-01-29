using Sandbox;

namespace ImposterSyndrome.Systems.States
{
	public partial class PlayingState : BaseState
	{
		[Net] public override string StateName => "Playing";
		public override float StateDuration { get; set; } = 30;
	}
}
