using Godot;

namespace youmustlose.entities {
	public class Switch : LevelEventSignaller, ILevelEventListener {
		[Export] public string onEvent;
		[Export] public string offEvent;
		[Export] public string interactionEvent = "interact";
		[Export] public bool state = false;
		[Export] public string gate = null;
		[Export] public bool oneTime = false;

		private bool canInteract = false;

		private bool gated = false;

		private bool canUpdate = true;

		private AnimatedSprite sprite;

		public override void _Ready () {
			if (gate != null) {
				gated = true;
			}

			sprite = GetNode<AnimatedSprite>("AnimatedSprite");
			updateSprite();
		}

		public void onAreaEntered (Area2D other) {
			canInteract = true;
			updateSprite();
		}

		public void onAreaExited (Area2D other) {
			canInteract = false;
			updateSprite();
		}

		public void onLevelEvent (string eventName) {
			if (canInteract && eventName == interactionEvent && !gated && canUpdate) {
				raiseLevelEvent(state? offEvent : onEvent);
				state = !state;
				if (oneTime) {
					canUpdate = false;
				}
			} else if (gated && eventName == gate) {
				gated = !gated;
			}
			updateSprite();
		}

		private void updateSprite () {
			if (state) {
				sprite.Animation = "on";
			} else {
				sprite.Animation = "off";
			}

			if (gated || !canUpdate) {
				sprite.Animation = "gated_" + sprite.Animation;
			} else if (canInteract) {
				sprite.Animation = "highlight_" + sprite.Animation;
			}
		}
	}
}