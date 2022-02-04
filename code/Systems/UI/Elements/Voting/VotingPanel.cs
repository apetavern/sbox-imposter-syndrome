using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ImposterSyndrome.Systems.UI
{
	public class VotingPanel : Panel
	{
		public static VotingPanel Instance { get; set; }
		private Label TimerLabel { get; set; }

		public VotingPanel()
		{
			StyleSheet.Load( "/Systems/UI/Elements/Voting/VotingPanel.scss" );
			Instance = this;
		}

		public void Show( bool shouldShow )
		{
			DeleteChildren( true );

			SetClass( "visible", shouldShow );

			if ( !shouldShow )
				return;

			var header = Add.Panel( "header" );
			header.Add.Label( "Who is the imposter?", "Title" );
			TimerLabel = header.Add.Label( "0s", "Timer" );

			var container = Add.Panel( "container" );

			foreach ( var player in ImposterSyndrome.Instance?.Players )
			{
				container.AddChild( new PlayerPanel( player ) );
			}

			var footer = Add.Panel( "footer" );
			footer.Add.Button( "Skip", () => Log.Info( "SKIP" ) );
		}

		public override void Tick()
		{
			base.Tick();

			if ( ImposterSyndrome.Instance.CurrentState is null || TimerLabel is null )
				return;

			var stateEndTime = ImposterSyndrome.Instance.CurrentState.StateEndTime;
			TimerLabel.Text = MathX.Clamp( (int)(stateEndTime - Time.Now), 0, 500 ).ToString() + "s";
		}
	}
}
