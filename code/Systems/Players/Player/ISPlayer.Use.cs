using ImposterSyndrome.Systems.Entities;
using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public enum UseType
	{
		Use,
		Report,
		Kill
	}

	public partial class ISPlayer
	{
		public IEntityUse GetNearestUsable( UseType useType )
		{
			var ents = Physics.GetEntitiesInSphere( Position, GameConfig.InteractionRadius )
				.Where( ent => ent.Owner != Owner )
				.OrderBy( ent => Vector3.DistanceBetween( ent.Position, Position ) )
				.OfType<IEntityUse>().ToList();

			return ents.FirstOrDefault( ent => ent.IsUsable( this, useType ) );
		}

		[ServerCmd]
		public static void UseNearestEntity( UseType entityUseType )
		{
			if ( Host.IsClient )
				return;

			if ( ConsoleSystem.Caller.Pawn is not ISPlayer player )
				return;

			var usingEnt = player.GetNearestUsable( entityUseType );

			usingEnt?.OnUse( player, entityUseType );
		}
	}
}
