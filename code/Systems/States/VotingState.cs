using Sandbox;

namespace ImposterSyndrome.Systems.States
{
	public partial class VotingState : BaseState
	{
		[Net] public override string StateName => "VotingState";
		public override float StateDuration { get; set; } = 30;
	}
}
