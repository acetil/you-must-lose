using Godot;
using youmustlose.entities;

namespace youmustlose.level_logic.audio {
	public abstract class LevelSound : Node, ILevelEventListener {
		[Signal]
		public delegate void ReloadLevel (int reloadNum);

		[Signal]
		public delegate void NextLevel ();

		[Signal]
		public delegate void Pause ();

		[Export] public string audioBus = "Master";

		protected AudioStreamPlayer audioPlayer;
		public int reloadNum;
		public override void _Ready () {
			audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
			audioPlayer.Bus = audioBus;
		}

		
		protected SignalAwaiter playSound (AudioStream sound) {
			audioPlayer.Stream = sound;
			audioPlayer.Play();
			return ToSignal(audioPlayer, "finished");
		}

		public abstract void onLevelStart ();
		public abstract void onSuccess ();
		public abstract void onDie ();

		public void onLevelEvent (string eventName) {
			
		}

		protected void reloadLevel () {
			EmitSignal(nameof(ReloadLevel), reloadNum + 1);
		}

		protected void nextLevel () {
			EmitSignal(nameof(NextLevel));
		}

		protected void pause () {
			EmitSignal(nameof(Pause));
		}
	}
}