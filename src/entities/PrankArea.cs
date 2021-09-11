using System;
using Godot;

namespace youmustlose.entities {
	public class PrankArea : LevelEventSignaller {
		[Export] public string eventName;

		public void onAreaEntered (Area2D other) {
			Console.WriteLine("Entered prank area!");
			raiseLevelEvent(eventName);
		}
	}
}