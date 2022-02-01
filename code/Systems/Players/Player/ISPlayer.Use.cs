using ImposterSyndrome.Systems.Entities;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer
	{
		public IEntityUse LocateUsable()
		{
			var ents = Physics.GetEntitiesInSphere( Position, GameConfig.InteractionRadius ).OfType<IEntityUse>().ToList();

			return ents.FirstOrDefault( ent => ent.IsUsable( this ) );
		}

		public IEntityUse LocateUsable<T>()
		{
			if ( typeof( T ) is not IEntityUse )
				return null;

			var ents = Physics.GetEntitiesInSphere( Position, GameConfig.InteractionRadius ).OfType<T>().ToList();

			return ents.Cast<IEntityUse>().FirstOrDefault( ent => ent.IsUsable( this ) );
		}

		[ServerCmd]
		public static void UseNearestEntity()
		{
			if ( Host.IsClient )
				return;

			if ( ConsoleSystem.Caller.Pawn is not ISPlayer player )
				return;

			var usingEnt = player.LocateUsable();
			usingEnt?.OnUse( player );
		}
	}
}
