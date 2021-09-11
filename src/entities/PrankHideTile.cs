using System;
using System.Collections.Generic;
using Godot;

namespace youmustlose.entities {
	public class PrankHideTile : TileMap,ILevelEventListener {

		[Export] public string[] hideEvent;

		private List<string> eventNames;
		
		public override void _Ready () {
			eventNames = new List<string>(hideEvent);
		}

		public void onLevelEvent (string eventName) {
			if (eventNames.Contains(eventName)) {
				CollisionLayer = 0;
				CollisionMask = 0;
				Hide();
			}
			
		}
	}
}