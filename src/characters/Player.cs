using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace youmustlose.characters {
    public class Player : KinematicBody2D {

        [Export] public float relativeMovementForce = 20.0f;
        [Export] public float airMoveMultiplier = 0.75f;

        [Export] public Vector2 gravityRel = new Vector2(0.0f, 10.0f);

        [Export] public Vector2 jumpImpulseNorm = new Vector2(0.0f, 100.0f);

        [Export] public float coyoteTime = 0.1f;

        [Export] public int maxJumps = 2;

        [Export] public float horizontalFriction = 0.1f;

        [Export] public float minXVel = 0.001f;

        [Export] public string interactionEvent = "interact";

        [Export] public float floorTolerance = 0.0f;

        [Export] public bool reversed = false;

        [Signal]
        public delegate void ReloadLevel ();

        [Signal] 
        public delegate void NextLevel ();

        [Signal]
        public delegate void OnJump (bool isDouble);

        [Signal]
        public delegate void OnInteract (string eventName);

        private Vector2 velocity = new Vector2(0, 0);
        private Vector2 gravity;
        private Vector2 movementVel = new Vector2(0, 0);

        private const float SPEED_MULT = 10.0f;
        private const float GRAVITY_MULT = 100.0f;

        private float movementForce = 0.0f;

        private bool jumped = false;

        private bool grounded = false;

        private int jumps = 0;

        private float timeSinceGrounded = 0.0f;

        private bool movingDeathPrimed = false;

        private bool hasInteracted = false;
        
        // Called when the node enters the scene tree for the first time.
        public override void _Ready () {
            movementForce = relativeMovementForce * SPEED_MULT;
            gravity = gravityRel * GRAVITY_MULT;
            jumpImpulseNorm.y *= -1;
        }

        private Vector2 handleInput (float delta) {
            var deltaVel = new Vector2(0, 0);
            if (Input.IsActionPressed("movement_left")) {
                deltaVel.x -= movementForce * delta * (grounded ? 1 : airMoveMultiplier);
            }

            if (Input.IsActionPressed("movement_right")) {
                deltaVel.x += movementForce * delta * (grounded ? 1 : airMoveMultiplier);
            }

            if (Input.IsActionPressed("movement_jump")) {
                if (jumps < maxJumps && !jumped) {
                    Console.WriteLine("Jumped!");
                    deltaVel.y -= velocity.y;
                    deltaVel += jumpImpulseNorm;
                    jumps++;
                    jumped = true;
                    grounded = false;
                    EmitSignal(nameof(OnJump), jumps != 1);
                }
            } else {
                jumped = false;
            }

            if (Input.IsActionPressed("player_interact")) {
                if (!hasInteracted) {
                    EmitSignal(nameof(OnInteract), interactionEvent);
                    hasInteracted = true;
                }
            } else {
                hasInteracted = false;
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
            if (checkOnFloor()) {
                jumps = 0;
                timeSinceGrounded = 0.0f;
                grounded = true;
            } else {
                timeSinceGrounded += delta;
                if (timeSinceGrounded > coyoteTime && jumps == 0) {
                    jumps = 1;
                    grounded = false;
                }
            }
        }

        public void onAreaEntered(Area2D other) {
            Console.WriteLine("Collision detected!");

            if (other.IsInGroup("goals")) {
                Console.WriteLine("Collision detected with goal!");
                if (!reversed) {
                    EmitSignal(nameof(ReloadLevel));
                } else {
                    EmitSignal(nameof(NextLevel));
                }
            } else if (other.IsInGroup("enemies")) {
                Console.WriteLine("Collision detected with enemy!");
                die();
            } else if (other.IsInGroup("death")) {
                die();
            } else if (other.IsInGroup("prime_moving")) {
                Console.WriteLine("Moving death primed!");
                movingDeathPrimed = true;
            }
        }

        public void onAreaExited (Area2D other) {
            if (other.IsInGroup("prime_moving")) {
                Console.WriteLine("Moving death unprimed!");
                movingDeathPrimed = false;
            }
        }

        public void onBodyEntered (Node other) {
            Console.WriteLine("Detected body collision!");
            if (other.IsInGroup("moving") && movingDeathPrimed) {
                die();
            }
        }

        private bool checkOnFloor () {
            var upVec = Vector2.Up;
            for (var i = 0; i < GetSlideCount(); i++) {
                var coll = GetSlideCollision(i);
                var angle = upVec.AngleTo(coll.Normal);
                if (Math.Abs(angle) <= floorTolerance) {
                    return true;
                }
                Console.WriteLine("Upvec: " + upVec + ", normal: " + coll.Normal + ", angle to: " + angle);
            }
            Console.WriteLine("No floor collision!");
            return false;
        }

        private void die () {
            if (!reversed) {
                EmitSignal(nameof(NextLevel));
            } else {
                EmitSignal(nameof(ReloadLevel));
            }
        }
    }
}
