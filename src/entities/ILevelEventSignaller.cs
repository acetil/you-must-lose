using Godot;

namespace youmustlose.entities {
	public interface ILevelEventSignaller {
		[Signal] public delegate void LevelEvent (string eventName);
	}
}