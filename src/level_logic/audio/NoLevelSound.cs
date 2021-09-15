namespace youmustlose.level_logic.audio {
	public class NoLevelSound : LevelSound {
		public override void onLevelStart () {
			
		}

		public override void onSuccess () {
			reloadLevel();
		}

		public override void onDie () {
			nextLevel();
		}
	}
}