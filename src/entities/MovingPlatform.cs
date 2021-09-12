using System;
using Godot;
using youmustlose.characters;

namespace youmustlose.entities {
	public class MovingPlatform : KinematicBody2D, ILevelEventListener {
		[Export]
		public float minX = 100.0f;
		[Export]
		public float maxX = 500.0f;

		[Export]
		public float velocity = 100;

		[Export] public float yPos;

		[Export] public string hideEvent;

		private int direction = 1;

		public override void _PhysicsProcess (float delta) {
			var vel = new Vector2();

			var doFlip = false;

			if (direction == 1 && maxX - Position.x < velocity * delta) {
				vel.x = (maxX - Position.x) / delta;
				doFlip = true;
			} else if (direction == -1 && Position.x - minX < velocity * delta) {
				vel.x = (Position.x - minX) / delta;
				doFlip = true;
			} else {
				vel.x = velocity;
			}

			if (Math.Abs(Position.y - yPos) > float.Epsilon) {
				vel.y = -clamp(Position.y - yPos, -velocity, velocity);
			} else {
				vel.y = 0;
			}

			vel.x *= direction;
			var coll = MoveAndCollide(vel * delta);
			
			if (doFlip || coll != null && !(coll.Collider is Player)) {
				direction = -direction;
			}
			
			
			/*MoveAndSlide(vel);

			if (doFlip) {
				direction = -direction;
			}*/
		}
		private static float clamp (float val, float min, float max) {
			if (min > val) {
				return min;
			}
			return max < val ? max : val;
		}
		
		public void onLevelEvent (string eventName) {
			if (eventName == hideEvent) {
				SetProcess(false);
				SetPhysicsProcess(false);
				Hide();
				CollisionLayer = 0;
				CollisionMask = 0;
				foreach (var n in GetChildren()) {
					if (n is Area2D area2D) {
						area2D.CollisionLayer = 0;
						area2D.CollisionMask = 0;
					}
				}
			}
		}
	}
}