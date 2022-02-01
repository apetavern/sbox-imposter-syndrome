using System.Linq;
using Sandbox;
using ImposterSyndrome.Systems.Players;
using System.Collections.Generic;
using System;
using ImposterSyndrome.Systems.UI;
using ImposterSyndrome.Systems.Tasks;

namespace ImposterSyndrome.Systems.States
{
	public partial class PlayingState : BaseState
	{
		[Net] public override string StateName => "Playing";
		public override float StateDuration { get; set; } = 300;
		public List<ISPlayer> Imposters { get; set; } = new();

		public override void OnStateStarted()
		{
			base.OnStateStarted();

			foreach ( var player in Client.All.Select( cl => cl.Pawn as ISBasePlayer ) )
			{
				var newPawn = new ISPlayer();
				player.UpdatePawn( newPawn );

				ImposterSyndrome.Instance.Players.Add( newPawn );
			}

			AssignImposters();
			AssignTasks();
		}

		private void AssignTasks()
		{
			foreach ( var player in ImposterSyndrome.Instance.Players )
			{
				// Temporarily add all tasks. We can add a random selection of tasks later instead.
				foreach ( var task in Library.GetAll<BaseTask>().Where( x => !x.IsAbstract ) )
					player.AssignedTasks.Add( Library.Create<BaseTask>( task ).FlagAsFake( player.IsImposter ) );
			}
		}

		private void AssignImposters()
		{
			var imposterCount = MathX.CeilToInt( ImposterSyndrome.Instance.Players.Count * 0.25f );

			for ( int i = 0; i < imposterCount; i++ )
			{
				var player = ImposterSyndrome.Instance.Players.Where( player => !player.IsImposter ).OrderBy( _ => Guid.NewGuid() ).First();
				player.IsImposter = true;

				Imposters.Add( player );

				// TODO: LET OTHER IMPOSTERS KNOW WHO THE IMPOSTERS ARE.
			}
		}

		public override void OnSecond()
		{
			base.OnSecond();

			if ( !HasMinimumAlivePlayers() || !HasTasksOutstanding() )
				OnStateEnded();
		}

		private bool HasMinimumAlivePlayers()
		{
			var players = ImposterSyndrome.Instance.Players;
			var alivePlayers = players.Where( player => !player.IsImposter && player.LifeState == LifeState.Alive );

			if ( alivePlayers.Count() == 1 )
				return false;

			return true;
		}

		private bool HasTasksOutstanding()
		{
			if ( ISPlayer.GetAllPlayersTaskProgress() >= 100 )
				return false;

			return true;
		}

		public override void OnStateEnded()
		{
			base.OnStateEnded();

			ImposterSyndrome.UpdateState( new GameEndState() );
		}
	}
}
