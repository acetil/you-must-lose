using System;
using System.Collections.Generic;
using Godot;

namespace youmustlose.entities {
	public class RotatingNode : Node2D, ILevelEventListener, ILevelEventSignaller {
		
		[Signal]
		public delegate void LevelEvent (string eventName);
		
		[Export] public string rotateEvent;

		[Export] public float angularVelocity;

		private float targetRotation = 0;

		private bool isRotating = false;

		private List<ILevelEventListener> listeners = new List<ILevelEventListener>();

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
			}
		}

		public override void _Process (float delta) {
			var deltaRot = targetRotation * (float)Math.PI * 2 - Rotation;
			if (deltaRot < 0) {
				deltaRot += 2 * (float)Math.PI;
			}
			if (deltaRot != 0) {
				Rotation += Math.Min(deltaRot, angularVelocity * delta);
				if (Rotation > Math.PI * 2) {
					Rotation -= (float)Math.PI * 2;
				}

				if (Math.Abs(Rotation - targetRotation * (float) Math.PI * 2) < float.Epsilon) {
					isRotating = false;
				}
			}

		}

		public void onLevelEvent (string eventName) {
			Console.WriteLine("Received event: " + eventName);
			if (eventName == "prank" && !isRotating) {
				Console.WriteLine("Starting rotation!");
				targetRotation += 0.25f;
				if (targetRotation >= 1.0f) {
					targetRotation -= 1.0f;
				}

				isRotating = true;
			} else {
				foreach (var n in listeners) {
					n.onLevelEvent(eventName);
				}
			}
		}

		public void onLevelEventSent (string eventName) {
			raiseLevelEvent(eventName);
			onLevelEvent(eventName);
		}

		private void raiseLevelEvent (string eventName) {
			EmitSignal(nameof(LevelEvent), eventName);
		}
	}
}