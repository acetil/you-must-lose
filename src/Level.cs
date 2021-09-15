using System;
using System.Collections;
using System.Collections.Generic;
using Godot;
using youmustlose.entities;
using youmustlose.level_logic;
using youmustlose.level_logic.audio;

namespace youmustlose {
	public class Level : Node2D {

		[Export] public PackedScene nextLevel;
		[Export] public string levelName;
		
		[Signal] 
		public delegate void ChangeLevel (PackedScene scene);

		[Signal]
		public delegate void ReloadLevel (int reloadNum);

		public int reloadNum;

		private List<ILevelEventListener> listeners = new List<ILevelEventListener>();

		private List<IJumpListener> jumpListeners = new List<IJumpListener>();

		private LevelSound levelSound;
		
		public override void _Ready () {
			var children = GetChildren();

			foreach (var n in children) {
				// ReSharper disable once ConvertIfStatementToSwitchStatement
				if (n is ILevelEventListener listener) {
					listeners.Add(listener);
				}

				if (n is IJumpListener jumpListener) {
					jumpListeners.Add(jumpListener);
				}

				if (n is LevelEventSignaller node) {
					node.Connect(nameof(LevelEventSignaller.LevelEvent), this, nameof(onLevelEvent));
				}
			}

			levelSound = GetNode<LevelSound>("LevelAudio");
			if (levelSound != null) {
				levelSound.reloadNum = reloadNum;
				levelSound.onLevelStart();
				levelSound.Connect(nameof(LevelSound.NextLevel), this, nameof(onLevelChange));
				levelSound.Connect(nameof(LevelSound.ReloadLevel), this, nameof(onReload));
				levelSound.Connect(nameof(LevelSound.Pause), this, nameof(onPause));
			}
		}

		public void onReloadRequest () {
			levelSound.onSuccess();
		}

		public void onReload (int newReloadNum) {
			EmitSignal(nameof(ReloadLevel), newReloadNum);
		}

		public void onLevelChangeRequest () {
			levelSound.onDie();
		}

		public void onLevelChange () {
			EmitSignal(nameof(ChangeLevel), nextLevel);
		}

		public void onPause () {
			Console.WriteLine("Pausing!");
			GetTree().Paused = true;
		}

		public void onLevelEvent (string eventName) {
			Console.WriteLine("Raised event: " + eventName);
			foreach (var node in listeners) {
				node.onLevelEvent(eventName);
			}
		}

		public void onJump (bool isDouble) {
			foreach (var node in jumpListeners) {
				node.onJump(isDouble);
			}
		}

		public void onInteraction (string interactionEvent) {
			onLevelEvent(interactionEvent);
		}
	}
}