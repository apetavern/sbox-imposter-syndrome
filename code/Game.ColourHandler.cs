using Sandbox;
using System.Collections.Generic;
using ImposterSyndrome.Systems.Players;
using System.Linq;

namespace ImposterSyndrome
{
	public partial class ImposterSyndrome
	{
		private static List<Color> AvailableColors { get; set; } = new();

		public static void AssignPlayerColors()
		{
			foreach ( var player in Instance?.Players )
			{
				var color = Rand.FromList( AvailableColors );
				AvailableColors.Remove( color );

				player.UpdateColor( color );
			}
		}

		public static void ResetAvailableColors()
		{
			AvailableColors.Clear();
			AvailableColors.AddRange( GameConfig.AvailablePlayerColors );
		}
	}
}
