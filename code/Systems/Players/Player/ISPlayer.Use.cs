using ImposterSyndrome.Systems.Entities;
using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer
	{
		public IEntityUse GetNearestUsable()
		{
			var ents = Physics.GetEntitiesInSphere( Position, GameConfig.InteractionRadius ).OfType<IEntityUse>().ToList();

			return ents.FirstOrDefault( ent => ent.IsUsable( this ) );
		}

		public IEntityUse GetNearestUsable<T>() where T : IEntityUse
		{
			var ent = Physics.GetEntitiesInSphere( Position, GameConfig.InteractionRadius )
				.OfType<T>()
				.Where( ent => ent.IsUsable( this ) )
				.FirstOrDefault( ent => (ent as Entity).Owner != Owner );

			return ent;
		}

		[ServerCmd]
		public static void UseNearestEntity()
		{
			if ( Host.IsClient )
				return;

			if ( ConsoleSystem.Caller.Pawn is not ISPlayer player )
				return;

			var usingEnt = player.GetNearestUsable();
			usingEnt?.OnUse( player );
		}
	}
}
