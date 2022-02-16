using Sandbox;

namespace ImposterSyndrome.Systems.PostProcessing
{
	public static class PostEffects
	{
		public static StandardPostProcess Instance { get; set; }

		public static void Setup()
		{
			Instance = new StandardPostProcess();

			PostProcess.Add( Instance );
			AddDefaultStack();
		}

		public static void Reset()
		{
			PostProcess.Clear();
			AddDefaultStack();
		}

		static void AddDefaultStack()
		{
			// Vignette
			Instance.Vignette.Intensity = 0.85f;
			Instance.Vignette.Color = Color.Black;
			Instance.Vignette.Enabled = true;

			// Contrast
			Instance.Contrast.Contrast = 1.02f;
			Instance.Contrast.Enabled = true;

			// Depth of field
			// Instance.DepthOfField.Radius = 13.12f;
			// Instance.DepthOfField.FStop = 0.333f;
			// Instance.DepthOfField.FocalLength = 82;
			// Instance.DepthOfField.FocalPoint = 700;
			// Instance.DepthOfField.Enabled = true;
		}

		public static void DoDeath()
		{
			// Contast
			Instance.Contrast.Contrast = 0.95f;
			Instance.Contrast.Enabled = true;
		}
	}
}
