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
	[Hammer.EditorModel( "models/editor/arrow.vmdl" )]
	public partial class FishShoalEntity : TaskEntity
	{
		[Net] public override string UseName => "Fish";
		protected override Type TargetTaskType => typeof( FindWilson );
		protected override string ModelPath => "models/sphere.vmdl";
		private int MaxNumberOfFishInShoal { get; set; } = 5;
		private List<FishEntity> Fish { get; set; } = new();
		private AnimEntity Bait { get; set; }

		public override void Spawn()
		{
			base.Spawn();

			var numberOfFishInShoal = Rand.Int( 2, MaxNumberOfFishInShoal );

			for ( int i = 0; i < numberOfFishInShoal; i++ )
			{
				FishEntity fish = new FishEntity().AddToShoal( this );
				Fish.Add( fish );
			}
		}

		public override bool IsUsable( ISPlayer user, UseType useType )
		{
			Log.Info( "calling" );
			return true;
		}

		public override void OnTick()
		{
			base.OnTick();

			//DebugOverlay.Sphere( Position, 90f, Color.Red );
		}

		public override bool OnUse( ISPlayer user, UseType useType )
		{
			// Cast
			if ( Bait is null || !Bait.IsValid )
			{
				Bait = new AnimEntity( "models/float/float.vmdl" )
				{
					Position = user.Position + user.Rotation.Forward * 80
				};

				Log.Info( "Casting" );

				UseName = "Reel";
				return false;
			}

			// Reel
			if ( Fish.Any( x => x.IsHooked ) )
			{
				// Caught
				Log.Info( "Reeling | Caught a fish" );
				GetTaskInstance( user )?.MarkAsCompleted();
			}
			else
			{
				// Didn't catch
				Log.Info( "Reeling | Didn't catch anything" );
			}

			// Reset
			UseName = "Cast";
			Bait?.Delete();
			Bait = null;

			return false;
		}
	}
}
