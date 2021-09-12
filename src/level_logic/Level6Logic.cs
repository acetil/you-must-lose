using Godot;
using youmustlose.entities;

namespace youmustlose.level_logic {
	public class Level6Logic : Node2D, ILevelEventListener {
		[Export]
		public string startName;

		[Export] public float gravity = 9.8f;
		[Export] public float timeout = 1.0f;

		private Vector2 velocity = new Vector2();
		
		public override void _Ready () {
			Hide();
			SetProcess(false);
		}


		public override void _Process (float delta) {
			velocity.y += delta * gravity;
			Position += velocity;
		}


		public async void onLevelEvent (string eventName) {
			if (eventName == startName) {
				await ToSignal(GetTree().CreateTimer(timeout), "timeout");
				Show();
				SetProcess(true);
			}
		}
	}
}