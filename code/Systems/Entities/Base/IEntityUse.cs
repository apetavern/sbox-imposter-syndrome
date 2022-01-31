using ImposterSyndrome.Systems.Players;

namespace ImposterSyndrome.Systems.Entities
{
	public interface IEntityUse
	{
		bool OnUse( ISPlayer user );
		bool IsUsable( ISPlayer user );
	}
}
