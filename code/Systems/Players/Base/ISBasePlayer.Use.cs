using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISBasePlayer
	{
		public Entity LocateUsable()
		{
			var ents = Physics.GetEntitiesInSphere( Position, GameConfig.InteractionRadius ).OfType<IUse>().ToList();

			return ents.FirstOrDefault() as Entity;
		}
	}
}
