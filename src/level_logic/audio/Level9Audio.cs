using Godot;

namespace youmustlose.level_logic.audio {
	public class Level9Audio : LevelSound {
		[Export] public AudioStream successLine;
		[Export] public AudioStream[] dieLines;
		[Export] public string firstRotationEvent;
		[Export] public AudioStream[] rotateLines;
		public override void onLevelStart () {
			
		}

		public override async void onSuccess () {
			if (reloadNum == 0) {
				pause();
				await playSound(successLine);
			}
			reloadLevel();
		}

		public override async void onDie () {
			pause();
			foreach (var i in dieLines) {
				await playSound(i);
			}
			nextLevel();
		}

		public override async void onLevelEvent (string eventName) {
			if (eventName == firstRotationEvent && reloadNum == 0) {
				foreach (var i in rotateLines) {
					await playSound(i);
				}
			}
		}
	}
}