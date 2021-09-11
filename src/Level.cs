using Godot;

namespace youmustlose {
	public class Level : Node2D {

		[Export] public PackedScene nextLevel;
		
		[Signal] 
		public delegate void ChangeLevel (PackedScene scene);

		[Signal]
		public delegate void ReloadLevel ();
		
		public override void _Ready () {
			
		}

		public void onReloadRequest () {
			EmitSignal(nameof(ReloadLevel));
		}

		public void onLevelChangeRequest () {
			EmitSignal(nameof(ChangeLevel), nextLevel);
		}
		
	}
}