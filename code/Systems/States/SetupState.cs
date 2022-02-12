using System.Linq;
using Sandbox;
using ImposterSyndrome.Systems.Players;
using System.Collections.Generic;
using System;
using ImposterSyndrome.Systems.Tasks;

namespace ImposterSyndrome.Systems.States
{
	public partial class SetupState : BaseState
	{
		[Net] public override string StateName => "Setup";
		public override float StateDuration { get; set; } = 10;
		public List<ISPlayer> Imposters { get; set; } = new();

		public override void OnStateStarted()
		{
			base.OnStateStarted();

			foreach ( var client in Client.All )
			{
				var player = client.Pawn as ISBasePlayer;

				var newPawn = new ISPlayer();
				player?.UpdatePawn( newPawn );



				var colorIndex = ImposterSyndrome.Instance.AssignedColors.FirstOrDefault( x => x.Key == client ).Value;
				newPawn.UpdateColor( GameConfig.AvailablePlayerColors[colorIndex] );

				ImposterSyndrome.Instance.Players.Add( newPawn );
			}

			AssignImposters();
			AssignTasks();

			OnStateEnded();
		}

		private void AssignTasks()
		{
			foreach ( var player in ImposterSyndrome.Instance.Players )
			{
				// Temporarily add all tasks. We can add a random selection of tasks later instead.
				foreach ( var task in Library.GetAll<BaseTask>().Where( x => !x.IsAbstract ) )
				{
					var taskInstance = Library.Create<BaseTask>( task ).FlagAsFake( player.IsImposter );

					if ( taskInstance is SubTask )
						continue;

					player.AssignedTasks.Add( taskInstance );
				}
			}
		}

		private void AssignImposters()
		{
			var imposterCount = MathX.CeilToInt( ImposterSyndrome.Instance.Players.Count * 0.25f );

			for ( int i = 0; i < imposterCount; i++ )
			{
				var player = ImposterSyndrome.Instance.Players.Where( player => !player.IsImposter ).OrderBy( _ => Guid.NewGuid() ).First();
				player.UpdateImposterStatus( true );

				Imposters.Add( player );

				// TODO: LET OTHER IMPOSTERS KNOW WHO THE IMPOSTERS ARE.
			}
		}

		public override void OnStateEnded()
		{
			base.OnStateEnded();

			ImposterSyndrome.UpdateState( new PlayingState() );
		}
	}
}
