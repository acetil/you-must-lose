using Godot;
using youmustlose.entities;

namespace youmustlose.level_logic {
	// Some of the worst code I've ever written
	public class Level8Logic : LevelEventSignaller, ILevelEventListener {

		private delegate bool EvaluateState (LevelState state);

		private readonly EvaluateState door1Func = state => state.areaA && !state.switchA;
		private readonly EvaluateState door2Func = state => state.areaB && state.switchA && state.switchB;
		private readonly EvaluateState door3Func = state => state.areaA;
		private readonly EvaluateState door4Func = state => state.areaA && !state.switchB;

		private DoorState oldState = new DoorState();
		private readonly DoorState doorState = new DoorState();

		private readonly LevelState levelState = new LevelState();
		
		public void onLevelEvent (string eventName) {
			var updated = false;
			switch (eventName) {
				case "enter_A":
					levelState.areaA = true;
					updated = true;
					break;
				case "exit_A":
					levelState.areaA = false;
					updated = true;
					break;
				case "enter_B":
					levelState.areaB = true;
					updated = true;
					break;
				case "exit_B":
					levelState.areaB = false;
					updated = true;
					break;
				case "on_A":
					levelState.switchA = true;
					updated = true;
					break;
				case "off_A":
					levelState.switchA = false;
					updated = true;
					break;
				case "on_B":
					levelState.switchB = true;
					updated = true;
					break;
				case "off_B":
					levelState.switchB = false;
					updated = true;
					break;
			}

			if (updated) {
				updateDoorState();
				sendUpdates();
				oldState = doorState.copy();
			}
		}

		private void updateDoorState () {
			doorState.door1 = door1Func(levelState);
			doorState.door2 = door2Func(levelState);
			doorState.door3 = door3Func(levelState);
			doorState.door4 = door4Func(levelState);
		}

		private void sendUpdates () {
			if (doorState.door1 != oldState.door1) {
				raiseLevelEvent(doorState.door1 ? "close_1" : "open_1");
			}
			if (doorState.door2 != oldState.door2) {
				raiseLevelEvent(doorState.door2 ? "close_2" : "open_2");
			}
			if (doorState.door3 != oldState.door3) {
				raiseLevelEvent(doorState.door3 ? "close_3" : "open_3");
			}
			if (doorState.door4 != oldState.door4) {
				raiseLevelEvent(doorState.door4 ? "close_4" : "open_4");
			}
		}

		private class LevelState {
			public bool areaA = false;
			public bool areaB = false;
			public bool switchA = false;
			public bool switchB = false;
		}

		private class DoorState {
			public bool door1 = false;
			public bool door2 = false;
			public bool door3 = false;
			public bool door4 = false;

			public DoorState copy () {
				var d = new DoorState();
				d.door1 = door1;
				d.door2 = door2;
				d.door3 = door3;
				d.door4 = door4;
				return d;
			}
		}
		
	}
}