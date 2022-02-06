using ImposterSyndrome.Systems.Players;
using ImposterSyndrome.Systems.States;
using Sandbox;
using System.ComponentModel;
using System.Linq;

namespace ImposterSyndrome.Systems.Entities
{
	public partial class DeadPlayerEntity : BaseUsable
	{
		protected override string ModelPath => "models/playermodel/terrysus.vmdl";

		[Browsable( false )]
		[Net] public bool HasBeenReported { get; set; }

		[Browsable( false )]
		[Net] public ISPlayer BodyOwner { get; set; }

		public override void Spawn()
		{
			base.Spawn();
			SetupPhysicsFromAABB( PhysicsMotionType.Static, new Vector3( -8, -8, 0 ), new Vector3( 8, 8, 30 ) );
		}

		public void UpdateFrom( ISPlayer player )
		{
			Position = player.Position;
			Rotation = Rotation.LookAt( Vector3.Up );
			BodyOwner = player;
		}

		public override bool IsUsable( ISPlayer user, UseType useType )
		{
			return !HasBeenReported && useType == UseType.Report;
		}

		public override bool OnUse( ISPlayer user, UseType useType )
		{
			ImposterSyndrome.UpdateState( new VotingState( user ) );
			HasBeenReported = true;

			return false;
		}

		public static void RemoveAll()
		{
			Entity.All.OfType<DeadPlayerEntity>().ToList().ForEach( player => player.Delete() );
		}
	}
}
