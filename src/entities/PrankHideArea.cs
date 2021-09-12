using Godot;

namespace youmustlose.entities {
	public class PrankHideArea : Node2D, ILevelEventListener {
		[Export] public string hideEvent;
		public void onLevelEvent (string eventName) {
			if (eventName == hideEvent) {
				var children = GetChildren();
				foreach (var i in children) {
					if (i is Area2D area2D) {
						area2D.CollisionMask = 0;
						area2D.CollisionLayer = 0;
					}
				}
				Hide();
				SetProcess(false);
				SetPhysicsProcess(false);
			}
		}
	}
}