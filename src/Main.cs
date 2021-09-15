using System;
using Godot;

namespace youmustlose {
	public class Main : Node2D {
		[Export] public PackedScene firstLevel;
		[Export] public bool debug = false;

		private PackedScene currentScene;

		private Level currentLevel = null;

		private GUI gui;


		public override void _Ready () {
			gui = GetNode<GUI>("GUI");
			if (debug) {
				gui.onStartGame();
			}
			//changeLevel(firstLevel);
		}


		public void onReloadLevel () {
			changeLevel(currentScene);
		}

		public void onChangeLevel (PackedScene nextLevel) {
			changeLevel(nextLevel);
		}

		public void startGame () {
			changeLevel(firstLevel);
		}
		
		private void changeLevel (PackedScene levelScene) {
			currentLevel?.QueueFree();
			
			var node = levelScene.Instance();

			if (node is Level level) {
				currentLevel = level;
				CallDeferred("add_child", node);
				//AddChild(node);
				setupSignals();
				currentScene = levelScene;
				gui.setLevelText(level.levelName);
			} else {
				Console.Error.WriteLine("Error: non-level scene!");
			}
		}

		private void setupSignals () {
			currentLevel.Connect("ChangeLevel", this, nameof(onChangeLevel));
			currentLevel.Connect("ReloadLevel", this, nameof(onReloadLevel));
		}
	}
}