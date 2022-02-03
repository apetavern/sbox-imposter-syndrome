using ImposterSyndrome.Systems.Entities;
using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.States
{
	public partial class VotingState : BaseState
	{
		[Net] public override string StateName => "Voting";
		public override float StateDuration { get; set; } = 30;

		public override void OnStateStarted()
		{
			// Send these over to the players HUDs.
			PlayerHudEntity.ShowVotingScreen( true );

			ISBasePlayer.ReturnAllToCampfire();
		}

		public override void OnStateEnded()
		{
			ImposterSyndrome.UpdateState( new PlayingState() );

			PlayerHudEntity.ShowVotingScreen( false );

			// Delete these dead players.
			DeadPlayerEntity.RemoveAll();
		}
	}
}
