using System;
using System.Collections.Generic;
using Godot;

namespace youmustlose.entities {
	public class EventPassthrough : LevelEventSignaller, ILevelEventListener, IJumpListener {

		private List<ILevelEventListener> listeners = new List<ILevelEventListener>();
		//private List<IJumpListener> jumpListeners = new List<IJumpListener>();
		public override void _Ready () {
			foreach (var n in GetChildren()) {
				// ReSharper disable once ConvertIfStatementToSwitchStatement
				if (n is ILevelEventListener listener) {
					listeners.Add(listener);
				}

				/*if (n is IJumpListener jumpListener) {
					jumpListeners.Add(jumpListener);
				}*/

				if (n is LevelEventSignaller node) {
					node.Connect(nameof(LevelEvent), this, nameof(onLevelEventSent));
				}

				if (n is Node node1 && n is ILevelEventSignaller) {
					node1.Connect("LevelEvent", this, nameof(onLevelEventSent));
				}
			}
		}

		public void onLevelEvent (string eventName) {
			Console.WriteLine("Passing through event!");
			foreach (var n in listeners) {
				n.onLevelEvent(eventName);
			}
		}

		public void onLevelEventSent (string eventName) {
			raiseLevelEvent(eventName);
			//onLevelEvent(eventName);
		}

		public void onJump (bool isDouble) {
			onLevelEvent("prank");
		}
	}
}