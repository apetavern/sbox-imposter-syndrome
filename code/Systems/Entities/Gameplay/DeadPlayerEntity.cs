﻿using ImposterSyndrome.Systems.Players;
using Sandbox;

namespace ImposterSyndrome.Systems.Entities
{
	public partial class DeadPlayerEntity : AnimEntity, IEntityUse
	{
		[Net] public bool HasBeenReported { get; set; }

		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/playermodel/terrysus.vmdl" );
			SetupPhysicsFromAABB( PhysicsMotionType.Static, new Vector3( -8, -8, 0 ), new Vector3( 8, 8, 30 ) );
		}

		public void UpdateFrom( ISPlayer player )
		{
			Position = player.Position;
			Rotation = Rotation.LookAt( Vector3.Up );
		}

		public bool IsUsable( ISPlayer user, UseType useType )
		{
			return !HasBeenReported && useType == UseType.Report;
		}

		public bool OnUse( ISPlayer user, UseType useType )
		{
			Log.Info( "DEAD BODY REPORTED" );

			ISPlayer.ReturnAllToCampfire();
			HasBeenReported = true;

			return false;
		}
	}
}