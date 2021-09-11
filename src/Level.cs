using System.Collections;
using System.Collections.Generic;
using Godot;
using youmustlose.entities;

namespace youmustlose {
	public class Level : Node2D {

		[Export] public PackedScene nextLevel;
		
		[Signal] 
		public delegate void ChangeLevel (PackedScene scene);

		[Signal]
		public delegate void ReloadLevel ();

		private List<ILevelEventListener> listeners = new List<ILevelEventListener>();
		
		public override void _Ready () {
			var children = GetChildren();

			foreach (var n in children) {
				// ReSharper disable once ConvertIfStatementToSwitchStatement
				if (n is ILevelEventListener listener) {
					listeners.Add(listener);
				}

				if (n is ILevelEventSignaller && n is Node node) {
					node.Connect(nameof(ILevelEventSignaller.LevelEvent), this, nameof(onLevelEvent));
				}
			}
		}

		public void onReloadRequest () {
			EmitSignal(nameof(ReloadLevel));
		}

		public void onLevelChangeRequest () {
			EmitSignal(nameof(ChangeLevel), nextLevel);
		}

		public void onLevelEvent (string eventName) {
			foreach (var node in listeners) {
				node.onLevelEvent(eventName);
			}
		}
		
	}
}