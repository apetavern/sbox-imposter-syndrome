using ImposterSyndrome.Systems.Entities;
using ImposterSyndrome.Systems.Players;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace ImposterSyndrome.Systems.UI
{
	public class DynamicInteractionButton : Panel
	{
		private IEntityUse LastUsableEntity { get; set; }
		private Label ButtonLabel { get; set; }

		public DynamicInteractionButton( string icon, Action onClick )
		{
			StyleSheet.Load( "/Systems/UI/Gameplay/Elements/InteractionButtons/ButtonStyles.scss" );

			ButtonLabel = Add.Label( "" );
			Add.Label( icon, "icon" );

			AddEventListener( "onclick", onClick );
		}

		public override void Tick()
		{
			base.Tick();

			var usableEntity = (Local.Pawn as ISPlayer)?.GetNearestUsable( UseType.Use );

			if ( LastUsableEntity != usableEntity )
			{
				SetClass( "enabled", usableEntity is not null );
				LastUsableEntity = usableEntity;

				if ( usableEntity is null )
				{
					ButtonLabel.Text = "";
					return;
				}

				ButtonLabel.Text = LastUsableEntity.UseName;
			}
		}
	}
}
