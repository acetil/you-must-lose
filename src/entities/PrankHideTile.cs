using Godot;

namespace youmustlose.entities {
	public class PrankHideTile : TileMap,ILevelEventListener {

		[Export]
		public string hideEvent = "prank";
		public void onLevelEvent (string eventName) {
			if (eventName == hideEvent) {
				CollisionLayer = 0;
				CollisionMask = 0;
				Hide();
			}
			
		}
	}
}