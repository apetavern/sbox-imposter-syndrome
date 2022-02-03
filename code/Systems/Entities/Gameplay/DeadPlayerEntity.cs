using ImposterSyndrome.Systems.Players;
using Sandbox;
using System.ComponentModel;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	public partial class DeadPlayerEntity : AnimEntity, IEntityUse
	{
		[Browsable( false )]
		[Net] public bool HasBeenReported { get; set; }

		[Browsable( false )]
		[Net] public ISPlayer BodyOwner { get; set; }

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
			BodyOwner = player;
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

		public static void RemoveAll()
		{
			Entity.All.OfType<DeadPlayerEntity>().ToList().ForEach( player => player.Delete() );
		}
	}
}
