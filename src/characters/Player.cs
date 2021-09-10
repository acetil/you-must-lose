using System;
using Godot;

namespace youmustlose.characters {
    public class Player : KinematicBody2D {

        [Export] public float relativeMovementSpeed = 20.0f;

        [Export] public Vector2 gravityRel = new Vector2(0.0f, 10.0f);

        [Export] public Vector2 jumpImpulseNorm = new Vector2(0.0f, 100.0f);

        [Export] public int maxJumps = 2;

        private Vector2 velocity = Vector2.Zero;
        private Vector2 gravity;
        private Vector2 movementVel = Vector2.Zero;

        private const float SPEED_MULT = 1000.0f;
        private const float GRAVITY_MULT = 100.0f;

        private float movementSpeed = 0.0f;

        private bool jumped = false;

        private int jumps = 0;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready () {
            movementSpeed = relativeMovementSpeed * SPEED_MULT;
            gravity = gravityRel * GRAVITY_MULT;
            jumpImpulseNorm.y *= -1;
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
            deltaVel += movementVel;
            return deltaVel;
        }

        public override void _PhysicsProcess(float delta) {
            velocity += handleInput(delta);
            velocity += gravity * delta;
            var coll  = MoveAndCollide(velocity * delta);

            if (coll != null) {
                Console.WriteLine("Vel: " + velocity);
                velocity = velocity.Slide(coll.Normal);
                Console.WriteLine("Vel: " + velocity);
                if (coll.Normal.x == 0) {
                    jumps = 0;
                }

                velocity = MoveAndSlide(velocity);
            }
        }
    }
}
