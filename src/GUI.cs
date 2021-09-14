using Godot;

namespace youmustlose {
	public class GUI : Control {
		private Label levelLabel;
		public override void _Ready () {
			levelLabel = GetNode<Label>("LevelLabel");
		}

		public void setLevelText (string text) {
			levelLabel.Text = text;
		}
	}
}