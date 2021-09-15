using System;
using Godot;
using youmustlose.entities;

namespace youmustlose.level_logic {
	public class Level7Logic : Area2D, ILevelEventSignaller {
		[Signal]
		public delegate void LevelEvent (string eventName);
		
		[Export]
		public float timeout = 1.0f;

		[Export] public float lowRamThreshold = 0.8f;
		[Export] public string lowRamEvent;

		private uint layers;
		private uint mask;
		private SceneTreeTimer timer;
		
		private Level7Label label;
		public override void _Ready () {
			layers = CollisionLayer;
			mask = CollisionMask;
			CollisionLayer = 0;
			CollisionMask = 0;
			//Hide();
			timer = GetTree().CreateTimer(timeout, false);
			timer.Connect("timeout", this, nameof(onTimeout));
			GetTree().CreateTimer(timeout * lowRamThreshold, false).Connect("timeout", this, nameof(onLowRam));
			label = GetNode<Level7Label>("DataLabel");
			label.timeToRamFinish = timeout;
		}

		public void onTimeout () {
			CollisionLayer = layers;
			CollisionMask = mask;
		}

		public void onLowRam () {
			EmitSignal(nameof(LevelEvent), lowRamEvent);
		}
	}
}