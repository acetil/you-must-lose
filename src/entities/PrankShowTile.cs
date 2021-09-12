using Godot;

namespace youmustlose.entities {
	public class PrankShowTile : TileMap, ILevelEventListener {
		[Export] public string eventName;

		private uint layers;
		private uint mask;

		public override void _Ready () {
			layers = CollisionLayer;
			mask = CollisionMask;

			CollisionLayer = 0;
			CollisionMask = 0;
			
			Hide();
		}

		public void onLevelEvent (string name) {
			if (eventName == name) {
				CollisionLayer = layers;
				CollisionMask = mask;
				Show();
			}
		}
	}
}