using Godot;

namespace youmustlose.entities {
	public class Switch : LevelEventSignaller, ILevelEventListener {
		[Export] public string onEvent;
		[Export] public string offEvent;
		[Export] public string interactionEvent = "interact";
		[Export] public bool state = false;
		[Export] public string gate = null;

		private bool canInteract = false;

		private bool gated = false;

		public override void _Ready () {
			if (gate != null) {
				gated = true;
			}
		}

		public void onAreaEntered (Area2D other) {
			canInteract = true;
		}

		public void onAreaExited (Area2D other) {
			canInteract = false;
		}

		public void onLevelEvent (string eventName) {
			if (canInteract && eventName == interactionEvent && !gated) {
				raiseLevelEvent(state? offEvent : onEvent);
				state = !state;
			} else if (gated && eventName == gate) {
				gated = !gated;
			}
		}
	}
}