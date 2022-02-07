using ImposterSyndrome.Systems.Players;

namespace ImposterSyndrome.Systems.Entities
{
	public interface IEntityUse
	{
		string UseName { get; set; }
		bool OnUse( ISPlayer user, UseType useType );
		bool IsUsable( ISPlayer user, UseType useType );
	}
}
