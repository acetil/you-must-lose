using Godot;

namespace youmustlose.entities {
	public class Switch : LevelEventSignaller, ILevelEventListener {
		[Export] public string onEvent;
		[Export] public string offEvent;
		[Export] public string interactionEvent = "interact";
		[Export] public bool state = false;

		private bool canInteract = false;

		public void onAreaEntered (Area2D other) {
			canInteract = true;
		}

		public void onAreaExited (Area2D other) {
			canInteract = false;
		}

		public void onLevelEvent (string eventName) {
			if (canInteract && eventName == interactionEvent) {
				raiseLevelEvent(state? offEvent : onEvent);
				state = !state;
			}
		}
	}
}