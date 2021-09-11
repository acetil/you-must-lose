using System;
using Godot;

namespace youmustlose.characters {
    public class Player : KinematicBody2D {

        [Export] public float relativeMovementForce = 20.0f;

        [Export] public Vector2 gravityRel = new Vector2(0.0f, 10.0f);

        [Export] public Vector2 jumpImpulseNorm = new Vector2(0.0f, 100.0f);

        [Export] public int maxJumps = 2;

        [Export] public float horizontalFriction = 0.1f;

        [Export] public float minXVel = 0.001f;

        [Signal]
        public delegate void ReloadLevel ();

        private Vector2 velocity = new Vector2(0, 0);
        private Vector2 gravity;
        private Vector2 movementVel = new Vector2(0, 0);

        private const float SPEED_MULT = 10.0f;
        private const float GRAVITY_MULT = 100.0f;

        private float movementForce = 0.0f;

        private bool jumped = false;

        private int jumps = 0;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready () {
            movementForce = relativeMovementForce * SPEED_MULT;
            gravity = gravityRel * GRAVITY_MULT;
            jumpImpulseNorm.y *= -1;
        }

        private Vector2 handleInput (float delta) {
            var deltaVel = new Vector2(0, 0);
            if (Input.IsActionPressed("movement_left")) {
                deltaVel.x -= movementForce * delta;
            }

            if (Input.IsActionPressed("movement_right")) {
                deltaVel.x += movementForce * delta;
            }

            if (Input.IsActionPressed("movement_jump")) {
                if (jumps < maxJumps && !jumped) {
                    Console.WriteLine("Jumped!");
                    deltaVel.y -= velocity.y;
                    deltaVel += jumpImpulseNorm;
                    jumps++;
                    jumped = true;
                }
            } else {
                jumped = false;
            }

            return deltaVel;
        }

        public override void _PhysicsProcess (float delta) {
            velocity += handleInput(delta);
            velocity += gravity * delta;

            var sign = velocity.x > 0;

            velocity.x -= horizontalFriction * velocity.x * delta;

            if (velocity.x > 0 != sign || Math.Abs(velocity.x) < minXVel) {
                velocity.x = 0;
            }
            
            velocity = MoveAndSlide(velocity, Vector2.Up);
            if (IsOnFloor()) {
                jumps = 0;
            }
        }

        public void onAreaEntered(Area2D other) {
            Console.WriteLine("Collision detected!");

            if (other.IsInGroup("goals")) {
                Console.WriteLine("Collision detected with goal!");
                EmitSignal(nameof(ReloadLevel));
            } else if (other.IsInGroup("enemies")) {
                Console.WriteLine("Collision detected with enemy!");
                EmitSignal(nameof(ReloadLevel));
            }
        }
    }
}
