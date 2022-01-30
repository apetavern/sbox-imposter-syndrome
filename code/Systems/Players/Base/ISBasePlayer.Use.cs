using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISBasePlayer
	{
		private int UseRadius { get; set; } = 60;

		public Entity LocateUsable()
		{
			var ents = Physics.GetEntitiesInSphere( Position, 50 ).OfType<IUse>().ToList();

			return ents.FirstOrDefault() as Entity;
		}
	}
}
