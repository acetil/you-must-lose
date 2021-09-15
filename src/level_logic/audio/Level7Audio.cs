using Godot;

namespace youmustlose.level_logic.audio {
	public class Level7Audio : LevelSound {
		[Export] public AudioStream[] startLines;
		[Export] public AudioStream[] lowRamLines;
		[Export] public string lowRamEvent;

		public override async void onLevelStart () {
			foreach (var i in startLines) {
				await playSound(i);
			}
		}

		public override void onSuccess () {
			reloadLevel();
		}

		public override void onDie () {
			nextLevel();
		}

		public override async void onLevelEvent (string eventName) {
			if (eventName == lowRamEvent) {
				foreach (var i in lowRamLines) {
					await playSound(i);
				}
			}
		}
	}
}