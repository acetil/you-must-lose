using System;
using Godot;

namespace youmustlose.level_logic.audio {
	public class Level10Audio : LevelSound {

		[Export] public AudioStream[] startLines;
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
	}
}