using System;
using Godot;

namespace youmustlose.entities {
	public class MovingPlatformArea : Area2D {
		public void onBodyEntered (Node node) {
			SetCollisionLayerBit(2, true);
			AddToGroup("prime_moving");
			Console.WriteLine("Entered body!");
		}

		public void onBodyExited (Node node) {
			SetCollisionLayerBit(2, false);
			RemoveFromGroup("prime_moving");
			Console.WriteLine("Exited body!");
		}
	}
}