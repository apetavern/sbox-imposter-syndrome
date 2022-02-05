namespace ImposterSyndrome
{
	public static class GameConfig
	{
		public static int MinimumPlayers = 6;
		public static float InteractionRadius = 60f;
		public static Color[] AvailablePlayerColors = new[]
		{
			Color.FromBytes(159,9,9), // Red
			Color.FromBytes(19,47,208), // Blue
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
