using Godot;
using youmustlose.entities;

namespace youmustlose.level_logic {
	public class Level4Logic : LevelEventSignaller, ILevelEventListener, IJumpListener {
		[Export] public string engageEvent = "prank";

		[Export] public string activateEvent = "prank";


		private bool engaged = false;
		
		public void onLevelEvent (string eventName) {
			if (eventName == engageEvent) {
				engaged = true;
			}
		}

		public void onJump (bool isDouble) {
			if (isDouble && engaged) {
				raiseLevelEvent(activateEvent);
			}
		}
	}
}