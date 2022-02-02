using ImposterSyndrome.Systems.Players;

namespace ImposterSyndrome.Systems.Entities
{
	public interface IEntityUse
	{
		bool OnUse( ISBasePlayer user, UseType useType );
		bool IsUsable( ISBasePlayer user, UseType useType );
	}
}
