using System;
using Godot;

namespace youmustlose.entities {
	public class Goal : Node2D {
		[Export] public float timeToAnim = 1.0f;
		[Export] public string animName = "animated";
		[Export] public bool disappear = false;
		private AnimatedSprite animations;

		public override void _Ready () {
			animations = GetNode<AnimatedSprite>("AnimatedSprite");
		}

		public async void onComplete () {
			await ToSignal(GetTree().CreateTimer(timeToAnim), "timeout");
			onTimer();
		}

		private void onTimer () {
			animations.Stop();
			animations.Play(animName);
			animations.Frame = 0;
		}

		private void onAreaEntered (Area2D other) {
			if (disappear) {
				Hide();
			}
		}
	}
}