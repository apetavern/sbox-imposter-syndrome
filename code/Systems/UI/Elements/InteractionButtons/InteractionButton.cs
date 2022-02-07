using ImposterSyndrome.Systems.Players;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace ImposterSyndrome.Systems.UI
{
	public class InteractionButton : Panel
	{
		public InteractionButton( string buttonText, string icon, UseType useType, Action onClick )
		{
			StyleSheet.Load( "/Systems/UI/Elements/InteractionButtons/InteractionButton.scss" );

			Add.Label( buttonText );
			Add.Label( icon, "icon" );

			AddEventListener( "onclick", onClick );
			BindClass( "enabled", () => (Local.Pawn as ISPlayer).GetNearestUsable( useType ) != null );
		}
	}
}
