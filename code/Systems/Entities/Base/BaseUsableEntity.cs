using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	public partial class BaseUsableEntity : AnimEntity, IEntityUse
	{
		[Net] public List<ISPlayer> UsedByPlayers { get; set; }
		public virtual string UseName { get; set; } = "Use";
		protected virtual string ModelPath { get; set; } = "models/citizen_props/cardboardbox01.vmdl";
		public virtual bool HideWorldModel { get; set; }
		private bool Debug { get; set; } = true;

		public override void Spawn()
		{
			base.Spawn();

			if ( HideWorldModel )
				return;

			SetModel( ModelPath );
			SetupPhysicsFromModel( PhysicsMotionType.Static );
		}

		public virtual bool IsUsable( ISPlayer user, UseType useType )
		{
			Log.Warning( "Entity of type BaseUsableEntity: IsUsable() not implemented." );
			return false;
		}

		public virtual bool OnUse( ISPlayer user, UseType useType )
		{
			Log.Warning( "Entity of type BaseUsableEntity: OnUse() not implemented." );
			return false;
		}

		[Event.Tick.Server]
		public virtual void OnTick()
		{
			DebugOverlay.Text( Position + Vector3.Up * 40f, Name );
		}

		public virtual void Reset()
		{
			UsedByPlayers.Clear();
			ToggleVisibility( true );
		}

		protected void DisableForPlayer( ISPlayer player )
		{
			UsedByPlayers.Add( player );
			ToggleVisibility( To.Single( player ), false );
		}

		protected bool HasBeenUsedBy( ISPlayer player )
		{
			return UsedByPlayers.Contains( player );
		}

		[ClientRpc]
		public void ToggleVisibility( bool shouldShow )
		{
			var renderAlpha = shouldShow ? 1 : 0.5f;
			RenderColor = RenderColor.WithAlpha( renderAlpha );
		}

		public static void ResetAll()
		{
			foreach ( var entity in All.OfType<BaseUsableEntity>() )
				entity.Reset();
		}
	}
}
