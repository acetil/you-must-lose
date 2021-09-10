using Godot;

namespace youmustlose.characters {
    public class Player : KinematicBody2D {

        [Export] public float relativeMovementSpeed = 20.0f;

        [Export] public Vector2 gravity = new Vector2(0.0f, 10.0f);

        private Vector2 velocity = Vector2.Zero;
        private Vector2 movementVel = Vector2.Zero;

        private const float SPEED_MULT = 1000.0f;

        private float movementSpeed = 0.0f;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready () {
            movementSpeed = relativeMovementSpeed * SPEED_MULT;
        }

        private Vector2 handleInput (float delta) {
            var deltaVel = -movementVel;
            movementVel.x = 0;
            if (Input.IsActionPressed("movement_left")) {
                movementVel.x -= movementSpeed * delta;
            }

            if (Input.IsActionPressed("movement_right")) {
                movementVel.x += movementSpeed * delta;
            }

            deltaVel += movementVel;
            return deltaVel;
        }

        public override void _PhysicsProcess(float delta) {
            velocity += handleInput(delta);
            velocity += gravity;
            velocity = MoveAndSlide(velocity);
        }
    }
}
