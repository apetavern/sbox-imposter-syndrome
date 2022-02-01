using Sandbox;
using System.Collections.Generic;

namespace ImposterSyndrome.Systems.UI
{
	public class Nametags
	{
		private Dictionary<Entity, Nametag> Tags { get; set; } = new();

		public Nametags()
		{
			Event.Register( this );
			Update();
		}

		public void Update()
		{
			if ( Host.IsClient )
			{
				foreach ( var player in ImposterSyndrome.Instance.Players )
				{
					if ( !Tags.ContainsKey( player ) )
					{
						var nametag = new Nametag();
						nametag.Player = player;

						Tags.Add( player, nametag );
					}
				}
			}
		}

		[Event.Tick]
		public void OnTick()
		{
			Update();
		}
	}
}
