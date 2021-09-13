using Godot;

namespace youmustlose.entities {
	public class AppearingDoor : KinematicBody2D, ILevelEventListener {
		[Export] public string hideEvent;
		public void onLevelEvent (string eventName) {
			if (eventName == hideEvent) {
				Hide();
				CollisionLayer = 0;
				CollisionMask = 0;
			}
		}
	}
}