using Godot;

namespace youmustlose.entities {
	public class AreaEnterExit : LevelEventSignaller {
		[Export] public string enterEvent;
		[Export] public string exitEvent;
		public void onAreaEnter (Area2D other) {
			raiseLevelEvent(enterEvent);
		}

		public void onAreaExit (Area2D other) {
			raiseLevelEvent(exitEvent);
		}
	}
}