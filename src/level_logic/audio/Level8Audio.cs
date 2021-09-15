using Godot;

namespace youmustlose.level_logic.audio {
	public class Level8Audio : LevelSound {

		[Export] public AudioStream[] startLines;
		[Export] public AudioStream successStream;
		[Export] public AudioStream[] dieLines;
		public override async void onLevelStart () {
			foreach (var i in startLines) {
				await playSound(i);
			}
		}

		public override async void onSuccess () {
			pause();
			await playSound(successStream);
			reloadLevel();
		}

		public override async void onDie () {
			pause();
			foreach (var i in dieLines) {
				await playSound(i);
			}
			nextLevel();
		}
	}
}