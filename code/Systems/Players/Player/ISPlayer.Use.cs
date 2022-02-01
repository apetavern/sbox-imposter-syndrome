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
			var ents = Physics.GetEntitiesInSphere( Position, GameConfig.InteractionRadius ).Where( ent => ent.Owner != Owner ).OfType<IEntityUse>().ToList();

			return ents.FirstOrDefault( ent => ent.IsUsable( this, useType ) );
		}

		/*public IEntityUse GetNearestUsable<T>( UseType useType ) where T : IEntityUse
		{
			var ent = Physics.GetEntitiesInSphere( Position, GameConfig.InteractionRadius )
				.OfType<T>()
				.Where( ent => ent.IsUsable( this, useType ) )
				.FirstOrDefault( ent => (ent as Entity).Owner != Owner );

			return ent;
		}*/

		[ServerCmd]
		public static void UseNearestEntity( UseType entityUseType )
		{
			if ( Host.IsClient )
				return;

			if ( ConsoleSystem.Caller.Pawn is not ISPlayer player )
				return;

			var usingEnt = player.GetNearestUsable( entityUseType );

			Log.Info( $"{player} wants to use {usingEnt} with use type {entityUseType}" );

			usingEnt?.OnUse( player, entityUseType );
		}
	}
}
