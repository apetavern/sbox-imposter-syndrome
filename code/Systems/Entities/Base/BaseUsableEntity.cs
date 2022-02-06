using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	public class TaskEntity : AnimEntity, IEntityUse
	{
		protected virtual Type TargetTaskType { get; set; }
		protected virtual string ModelPath { get; set; } = "models/citizen_props/cardboardbox01.vmdl";
		private bool Debug { get; set; } = true;

		public override void Spawn()
		{
			base.Spawn();

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
	}
}
