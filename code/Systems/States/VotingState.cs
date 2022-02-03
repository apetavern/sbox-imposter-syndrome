using Sandbox;

namespace ImposterSyndrome.Systems.States
{
	public partial class VotingState : BaseState
	{
		[Net] public override string StateName => "Voting";
		public override float StateDuration { get; set; } = 30;

		public override void OnStateEnded()
		{
			ImposterSyndrome.UpdateState( new PlayingState() );
		}
	}
}
