﻿using ImposterSyndrome.Systems.Entities;
using ImposterSyndrome.Systems.UI;
using Sandbox;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer : ISBasePlayer, IEntityUse
	{
		[Net, Local, Change] public bool IsImposter { get; private set; }
		private TimeSince TimeSinceKilled { get; set; }

		public override void OnKilled()
		{
			base.OnKilled();

			TimeSinceKilled = 0;

			PhysicsClear();

			LifeState = LifeState.Dead;

			new DeadPlayerEntity().UpdateFrom( this );

			UpdateRenderAlpha();
		}

		public override void Simulate( Client cl )
		{
			var controller = GetActiveController();
			controller?.Simulate( cl, this, GetActiveAnimator() );
		}

		public void OnIsImposterChanged()
		{
			PlayerHudEntity.Rebuild();
		}

		public bool IsUsable( ISPlayer user, UseType useType )
		{
			if ( LifeState == LifeState.Dead )
				return false;

			if ( !user.IsImposter )
				return false;

			return useType == UseType.Kill;
		}

		public bool OnUse( ISPlayer user, UseType useType )
		{
			switch ( useType )
			{
				case UseType.Kill:
					OnKilled();
					break;
			}

			return false;
		}

		public void UpdateImposterStatus( bool isImposter )
		{
			IsImposter = isImposter;
		}

		[ServerCmd]
		public static void ForceImposter( bool imposterStatus )
		{
			if ( ConsoleSystem.Caller.Pawn is not ISPlayer player )
				return;

			player.UpdateImposterStatus( imposterStatus );
		}
	}
}
