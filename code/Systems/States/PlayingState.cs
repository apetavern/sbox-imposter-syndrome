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
		public override float StateDuration { get; set; } = 30;
		public List<ISPlayer> Imposters { get; set; } = new();

		public override void OnStateStarted()
		{
			base.OnStateStarted();

			foreach ( var player in Client.All.Select( cl => cl.Pawn as ISBasePlayer ) )
			{
				var newPawn = new ISPlayer();
				player.UpdatePawn( newPawn );

				Game.Instance.Players.Add( newPawn );
			}

			AssignImposters();
			AssignTasks();

			// Update HUD on clients
			foreach ( var player in Game.Instance.Players )
				PlayerHudEntity.RebuildFromImposterStatus( player.IsImposter );
		}

		private void AssignTasks()
		{
			foreach ( var player in Game.Instance.Players )
			{
				// Temporarily add all tasks. We can add a random selection of tasks later instead.
				foreach ( var task in Library.GetAll<BaseTask>().Where( x => !x.IsAbstract ) )
					player.AssignedTasks.Add( Library.Create<BaseTask>( task ).FlagAsFake( player.IsImposter ) );
			}
		}

		private void AssignImposters()
		{
			var imposterCount = MathX.CeilToInt( Game.Instance.Players.Count * 0.25f );

			for ( int i = 0; i < imposterCount; i++ )
			{
				var player = Game.Instance.Players.Where( player => !player.IsImposter ).OrderBy( _ => Guid.NewGuid() ).First();
				player.IsImposter = true;

				Imposters.Add( player );

				// TODO: LET OTHER IMPOSTERS KNOW WHO THE IMPOSTERS ARE.
			}
		}

		public override void OnStateEnded()
		{
			base.OnStateEnded();

			Game.UpdateState( new GameEndState() );
		}
	}
}
