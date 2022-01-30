using System.Linq;
using Sandbox;
using ImposterSyndrome.Systems.Players;
using System.Collections.Generic;
using System;

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
		}

		private void AssignImposters()
		{
			var imposterCount = MathX.CeilToInt( Game.Instance.Players.Count * 0.25f );

			for ( int i = 0; i < imposterCount; i++ )
			{
				var player = Game.Instance.Players.OrderBy( _ => new Guid() ).Cast<ISPlayer>().Where( player => !player.IsImposter ).First();
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
