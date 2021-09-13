using Godot;
using youmustlose.entities;

namespace youmustlose.level_logic {
	public class Level10Logic : Area2D, ILevelEventListener {
		[Export] public string ev1;
		[Export] public string ev2;

		private bool ev1Done = false;
		private bool ev2Done = false;

		private uint layers = 0;
		private uint mask = 0;

		public override void _Ready () {
			layers = CollisionLayer;
			mask = CollisionMask;
			CollisionLayer = 0;
			CollisionMask = 0;
		}


		public void onLevelEvent (string eventName) {
			if (eventName == ev1) {
				ev1Done = true;
			} else if (eventName == ev2) {
				ev2Done = true;
			}

			if (ev1Done && ev2Done) {
				AddToGroup("enemies");
				CollisionLayer = layers;
				CollisionMask = mask;
			}
		}
	}
}