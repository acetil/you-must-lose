using Godot;

namespace youmustlose.level_logic.audio {
	public class Level9Audio : LevelSound {
		[Export] public AudioStream successLine;
		[Export] public AudioStream[] dieLines;
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
	}
}