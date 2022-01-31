using Sandbox.UI;
using System;

namespace ImposterSyndrome.Systems.UI
{
	public class InteractionButton : Button
	{
		public InteractionButton( string buttonText, System.Func<bool> enabledFunc, Action onClick )
		{
			StyleSheet.Load( "/Systems/UI/Elements/InteractionButtons/InteractionButton.scss" );

			Text = buttonText;
			AddEventListener( "onclick", onClick );
			BindClass( "enabled", enabledFunc );
		}
	}
}
