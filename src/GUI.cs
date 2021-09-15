using Godot;

namespace youmustlose {
	public class GUI : Control {
		[Signal]
		public delegate void StartGame ();
		private Label levelLabel;
		public override void _Ready () {
			levelLabel = GetNode<Label>("LevelLabel");
		}

		public void setLevelText (string text) {
			levelLabel.Text = text;
		}

		public void onStartGame () {
			EmitSignal(nameof(StartGame));
			RemoveChild(GetNode<Menu>("Menu"));
		}
	}
}