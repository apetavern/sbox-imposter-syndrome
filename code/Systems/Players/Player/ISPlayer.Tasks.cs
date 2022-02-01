using ImposterSyndrome.Systems.Tasks;
using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer
	{
		[Net] public List<BaseTask> AssignedTasks { get; set; }

		public float GetTotalTaskProgress()
		{
			float totalAmount = AssignedTasks.Count();

			if ( totalAmount <= 0 )
				totalAmount = 1;

			float completedAmount = AssignedTasks.Where( task => task.Status == TaskStatus.Complete ).Count();

			return (completedAmount / totalAmount) * 100;
		}

		public static float GetAllPlayersTaskProgress()
		{
			var players = Entity.All.OfType<ISPlayer>();

			float totalTaskAmount = players.Select( player => player.AssignedTasks.Count() ).Sum();

			if ( totalTaskAmount <= 0 )
				totalTaskAmount = 1;

			float totalCompletedAmount = 0;

			foreach ( var player in players )
				totalCompletedAmount += player.AssignedTasks.Where( task => task.Status == TaskStatus.Complete ).Count();

			return (totalCompletedAmount / totalTaskAmount) * 100; ;
		}
	}
}
