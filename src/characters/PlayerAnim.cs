using Godot;

namespace youmustlose.characters {
	public class PlayerAnim : AnimatedSprite {
		[Export] public string idleAnim = "idle";
		[Export] public string runningAnim = "running";
		[Export] public string jumpStartAnim = "jumping";
		[Export] public string jumpLandAnim = "landing";
		
		private bool grounded = true;
		private bool moving = false;

		private string swapOnComplete = null;

		public void onStartMove () {
			if (grounded) {
				Play(runningAnim);
				moving = true;
			}
		}

		public void onStopMove () {
			if (grounded) {
				Play(idleAnim);
				moving = false;
			}
		}

		public void onJump () {
			Play(jumpStartAnim);
			Frame = 0;
			swapOnComplete = jumpLandAnim;
			grounded = false;
		}

		public void onNotGrounded () {
			Play(jumpLandAnim);
			grounded = false;
		}

		public void onFacing (bool facingLeft) {
			FlipH = facingLeft;
		}

		public void onLand () {
			grounded = true;
			if (moving) {
				Play(runningAnim);
			} else {
				Play(idleAnim);
			}
		}
		
		public void onComplete () {
			if (swapOnComplete != null) {
				Play(swapOnComplete);
				swapOnComplete = null;
			}
		}
		
	}
}