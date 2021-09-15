using Godot;

namespace youmustlose.level_logic.audio {
	public class Level1Audio : SimpleLevelSound {
		[Export] public AudioStream secondSuccess;
		public override async void onSuccess () {
			if (reloadNum == 1) {
				pause();
				await playSound(secondSuccess);
			}
			base.onSuccess();
		}
	}
}