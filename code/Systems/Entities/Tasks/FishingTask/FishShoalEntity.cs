using ImposterSyndrome.Systems.Players;
using System.Linq;
using Sandbox;
using System.Collections.Generic;
using System;
using ImposterSyndrome.Systems.Tasks;

namespace ImposterSyndrome.Systems.Entities
{
	[Library( "is_tasks_fishshoal" )]
	[Hammer.EntityTool( "Fish Shoal", "ImposterSyndrome", "A group of fish to fish in." )]
	[Hammer.EditorModel( "models/float/float.vmdl" )]
	public partial class FishShoalEntity : TaskEntity
	{
		[Net] public override string UseName => "Fish";
		protected override Type TargetTaskType => typeof( CatchFish );
		protected override string ModelPath => "models/sphere.vmdl";
		private int MaxNumberOfFishInShoal { get; set; } = 5;
		public List<FishEntity> Fish { get; set; } = new();
		private FloatEntity Bait { get; set; }

		public override void Spawn()
		{
			base.Spawn();

			var numberOfFishInShoal = Rand.Int( 2, MaxNumberOfFishInShoal );

			for ( int i = 0; i < numberOfFishInShoal; i++ )
			{
				FishEntity fish = new FishEntity().AddToShoal( this );
				Fish.Add( fish );
			}

			SetInteractsAs( CollisionLayer.Debris );
		}

		public override bool IsUsable( ISPlayer user, UseType useType )
		{
			return true;
		}

		public override bool OnUse( ISPlayer user, UseType useType )
		{
			// Cast
			if ( Bait is null || !Bait.IsValid )
			{
				Bait = new FloatEntity()
				{
					Position = user.EyePosition + user.Rotation.Forward * 10,
					Owner = user,
					Shoal = this,
					Speed = 1000
				};

				UseName = "Reel";
				return false;
			}

			// Reel
			if ( Bait.Reel() == true )
			{
				// Caught
				Log.Info( "Reeling | Caught a fish" );
				GetTaskInstance( user )?.MarkAsCompleted();
			}
			else
			{
				// Didn't catch
				Log.Info( "Reeling | Didn't catch anything" );
				Bait?.Cleanup();
			}

			UseName = "Cast";

			Bait = null;

			return false;
		}
	}
}
