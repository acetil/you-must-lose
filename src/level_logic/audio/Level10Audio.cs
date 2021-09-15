using System;
using Godot;

namespace youmustlose.level_logic.audio {
	public class Level10Audio : LevelSound {

		[Export] public AudioStream[] startLines;
		[Export] public string killDevEvent;
		[Export] public AudioStream killDevLine;
		//[Export] public string killParrotEvent;
		[Export] public AudioStream killParrotLine;
		public override async void onLevelStart () {
			foreach (var i in startLines) {
				await playSound(i);
			}
		}

		public override void onSuccess () {
			reloadLevel();
		}

		public override async void onDie () {
			await playSound(killParrotLine);
			
			nextLevel();
		}

		public override void onLevelEvent (string eventName) {
			if (eventName == killDevEvent) {
				playSound(killDevLine);
			}
		}
	}
}