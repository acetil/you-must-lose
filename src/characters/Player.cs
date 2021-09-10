using Godot;

namespace youmustlose.characters {
    public class Player : KinematicBody2D {

        [Export] public float movementSpeed = 200.0f;

        private Vector2 movementVel = Vector2.Zero;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready () {
            
        }

        private void handleInput() {
            movementVel.x = 0;
            if (Input.IsActionPressed("movement_left")) {
                movementVel.x -= movementSpeed;
            }

            if (Input.IsActionPressed("movement_right")) {
                movementVel.x += movementSpeed;
            }
        }

        public override void _PhysicsProcess(float delta) {
            handleInput();
            movementVel = MoveAndSlide(movementVel);
        }
    }
}
