namespace ImposterSyndrome.Systems.Misc
{
	public interface IFogViewer
	{
		public Vector3 Position { get; set; }
		public float LineOfSightRadius { get; }
	}
}

