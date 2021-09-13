using System;
using Godot;
using youmustlose.characters;

namespace youmustlose.entities {
	public class SlidingDoor : KinematicBody2D, ILevelEventListener {
		[Export]
		public Vector2 startPos = new Vector2();
		[Export]
		public Vector2 endPos = new Vector2();

		[Export]
		public float velocity = 100;

		[Export] public float yPos;

		[Export] public string engageEvent;
		[Export] public string disengageEvent;

		[Export] public bool startEngaged = true;

		private Vector2 target;

		private bool moving = false;

		public override void _Ready () {
			if (startEngaged) {
				target = startPos;
			} else {
				target = endPos;
			}
		}

		public override void _PhysicsProcess (float delta) {
			if (moving) {
				var vel = new Vector2();

				//var doFlip = false;

				var disp = target - Position;

				if (disp.Length() < velocity * delta) {
					disp /= delta;
				} else {
					disp = disp.Normalized() * velocity;
				}

				if (Math.Abs(Position.y - yPos) > float.Epsilon) {
					vel.y = -clamp(Position.y - yPos, -velocity, velocity);
				} else {
					vel.y = 0;
				}

				var coll = MoveAndCollide(disp * delta);

				if ((Position - target).LengthSquared() < 0.0001) {
					Motion__syncToPhysics = true;
					moving = false;
				}
			}

			/*if (doFlip || coll != null && !(coll.Collider is Player)) {
				direction = -direction;
			}*/
			
			
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
			if (eventName == engageEvent) {
				target = endPos;
				GetNode<MovingPlatformArea>("Collider").engaged = true;
				if (!IsInGroup("moving")) {
					AddToGroup("moving");
				}

				moving = true;
			} else if (eventName == disengageEvent) {
				target = startPos;
				GetNode<MovingPlatformArea>("Collider").engaged = false;
				if (IsInGroup("moving")) {
					RemoveFromGroup("moving");
				}
				moving = true;
			}
		}
	}
}