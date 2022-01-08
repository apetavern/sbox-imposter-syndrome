using System;
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

		public override void Simulate()
		{
			Vector3 lookvec = new Vector3( Input.Forward, Input.Left, 0 );

			if ( lookvec.Length > 0 )
			{
				idealRotation = Rotation.LookAt( (Input.Rotation * lookvec).WithZ( 0 ), Vector3.Up );
			}

			DoRotation( idealRotation );

			SetParam( "walking", Velocity.Length > 1.5f );
		}

		public virtual void DoRotation( Rotation idealRotation )
		{
			float turnSpeed = 0.05f;

			Rotation = Rotation.Slerp( Rotation, idealRotation, WishVelocity.Length * Time.Delta * turnSpeed );
		}
	}
}
