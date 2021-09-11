using Godot;

namespace youmustlose.entities {
	public class LevelEventSignaller : Node {
		[Signal]
		public delegate void LevelEvent (string eventName);

		protected void raiseLevelEvent (string eventName) {
			EmitSignal(nameof(LevelEvent), eventName);
		}
	}
}