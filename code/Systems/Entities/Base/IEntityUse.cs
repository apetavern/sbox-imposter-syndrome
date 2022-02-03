using ImposterSyndrome.Systems.Players;

namespace ImposterSyndrome.Systems.Entities
{
	public interface IEntityUse
	{
		bool OnUse( ISPlayer user, UseType useType );
		bool IsUsable( ISPlayer user, UseType useType );
	}
}
