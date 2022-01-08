﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace Sussies.Systems.Player
{
	public partial class SussiesAnimator : PawnAnimator
	{
		Rotation idealRotation;
		bool IsTouchingLadder;
		Vector3 LadderNormal;

		public override void Simulate()
		{
			Vector3 lookvec = new Vector3( Input.Forward, Input.Left, 0 );

			if ( lookvec.Length > 0 )
			{
				idealRotation = Rotation.LookAt( (Input.Rotation * lookvec).WithZ( 0 ), Vector3.Up );
			}

			DoRotation( idealRotation );

			CheckLadder();

			AnimPawn.SetBodyGroup( 0, IsTouchingLadder ? 1 : 0 );

			SetParam( "onladder", IsTouchingLadder );

			SetParam( "walking", Velocity.Length > 1.5f );
		}

		public void CheckLadder()
		{
			var wishvel = new Vector3( Input.Forward, Input.Left, 0 );
			wishvel *= Input.Rotation.Angles().WithPitch( 0 ).ToRotation();
			wishvel = wishvel.Normal;

			if ( IsTouchingLadder )
			{
				if ( Input.Pressed( InputButton.Jump ) )
				{
					Velocity = LadderNormal * 100.0f;
					IsTouchingLadder = false;

					return;

				}
				else if ( GroundEntity != null && LadderNormal.Dot( wishvel ) > 0 )
				{
					IsTouchingLadder = false;

					return;
				}
			}

			const float ladderDistance = 1f;
			var start = Position;
			Vector3 end = start + (IsTouchingLadder ? (LadderNormal * -1.0f) : wishvel) * ladderDistance;

			WalkController controller = (AnimPawn as SussiesPlayer).Controller as WalkController;

			var girth = controller.BodyGirth * 0.5f;
			var mins = new Vector3( -girth, -girth, 0 );
			var maxs = new Vector3( +girth, +girth, controller.BodyHeight );

			var pm = Trace.Ray( start, end )
						.Size( mins, maxs )
						.HitLayer( CollisionLayer.All, false )
						.HitLayer( CollisionLayer.LADDER, true )
						.Ignore( Pawn )
						.Run();

			IsTouchingLadder = false;

			if ( pm.Hit && !(pm.Entity is ModelEntity me && me.CollisionGroup == CollisionGroup.Always) )
			{
				IsTouchingLadder = true;
				LadderNormal = pm.Normal;
			}

			
		}

		public virtual void DoRotation( Rotation idealRotation )
		{
			float turnSpeed = 0.05f;

			if ( IsTouchingLadder ) 
			{
				idealRotation = Rotation.LookAt( -LadderNormal, Vector3.Up );
			}

			Rotation = Rotation.Slerp( Rotation, idealRotation, WishVelocity.Length * Time.Delta * turnSpeed );
		}
	}
}
