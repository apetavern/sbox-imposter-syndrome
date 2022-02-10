using Sandbox;

namespace ImposterSyndrome.Systems.UI
{
	public class SceneAnimChild : AnimSceneObject
	{
		public SceneAnimChild( Model model, Transform transform ) : base( model, transform ) { }

		public void Tick()
		{
			Animate();
		}

		public void Animate()
		{
			Update( Time.Delta );
			SetAnimBool( "grounded", true );
		}
	}
}
