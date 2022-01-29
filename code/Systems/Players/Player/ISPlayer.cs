using Sandbox;

namespace ImposterSyndrome.Systems.Players
{
	public partial class ISPlayer : ISBasePlayer
	{
		[Net, Local] public bool IsImposter { get; set; }
	}
}
