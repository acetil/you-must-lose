using Godot;

namespace youmustlose.level_logic.audio {
	public class SimpleLevelSound : LevelSound {
		[Export] public AudioStream firstSuccess;
		[Export] public AudioStream dieStream;
		
		
		public override void onLevelStart () {
			
		}

		public override async void onSuccess () {
			if (reloadNum == 0) {
				pause();
				await playSound(firstSuccess);
			}
			reloadLevel();
		}

		public override async void onDie () {
			pause();
			await playSound(dieStream);
			nextLevel();
		}
	}
}