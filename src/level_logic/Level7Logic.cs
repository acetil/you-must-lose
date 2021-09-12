using Godot;

namespace youmustlose.level_logic {
	public class Level7Logic : Area2D {
		[Export]
		public float timeout = 1.0f;

		private uint layers;
		private uint mask;
		private SceneTreeTimer timer;
		public override void _Ready () {
			layers = CollisionLayer;
			mask = CollisionMask;
			CollisionLayer = 0;
			CollisionMask = 0;
			Hide();
			timer = GetTree().CreateTimer(this.timeout, false);
			timer.Connect("timeout", this, nameof(onTimeout));
		}

		public void onTimeout () {
			CollisionLayer = layers;
			CollisionMask = mask;
		}
	}
}