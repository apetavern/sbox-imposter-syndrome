using Sandbox;

namespace ImposterSyndrome
{
	public partial class GameConfig : BaseNetworkable
	{
		public static GameConfig Instance { get; set; }

		public GameConfig()
		{
			Instance = this;
		}

		[ServerCmd]
		public static void ReceiveMenuConfig( int imposterCount, int numOfTasks )
		{
			// TODO: Make this better when we have more config. options

			if ( Host.IsClient )
				return;

			if ( Instance is null )
				return;

			Instance.ImposterCount = imposterCount;
			Instance.NumberOfTasks = numOfTasks;
		}

		// Configurable
		[Net] public int ImposterCount { get; set; }
		[Net] public int NumberOfTasks { get; set; }

		// Non configurables
		public static int MinimumPlayers = 6;
		public static float InteractionRadius = 60f;
		public static Color[] AvailablePlayerColors = new[]
		{
			Color.FromBytes(233,0,0), // Red
			Color.FromBytes(0,89,244), // Blue
			Color.FromBytes(18,127,47), // Green
			Color.FromBytes(235,84,185), // Pink
			Color.FromBytes(242,87,0), // Orange
			Color.FromBytes(248,244,88), // Yellow
			Color.FromBytes(175,111,204), // Lilac
			Color.FromBytes(62,70,79), // Black
			Color.FromBytes(82,47,11), // Brown
			Color.FromBytes(80,240,56), // Lime
		};
	}
}
