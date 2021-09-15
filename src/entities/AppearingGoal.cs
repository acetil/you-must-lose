using Godot;

namespace youmustlose.entities {
	public class AppearingGoal : Goal, ILevelEventListener {
		[Export] public string showEvent;

		private uint layers;
		private uint mask;

		private Area2D area;

		public override void _Ready () {
			base._Ready();
			area = GetNode<Area2D>("Area2D");
			layers = area.CollisionLayer;
			mask = area.CollisionMask;
			area.CollisionLayer = 0;
			area.CollisionMask = 0;
			Hide();
		}

		public void onLevelEvent (string eventName) {
			if (eventName == showEvent) {
				area.CollisionLayer = layers;
				area.CollisionMask = mask;
				Show();
			}
		}
	}
}