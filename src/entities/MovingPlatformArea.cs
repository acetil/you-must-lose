using System;
using Godot;

namespace youmustlose.entities {
	public class MovingPlatformArea : Area2D {
		private bool isIn = false;
		private bool engagedVal = true;
		public bool engaged {
			private get => engagedVal;
			set {
				if (isIn && engagedVal && !value) {
					SetCollisionLayerBit(2, true);
					AddToGroup("prime_moving");
					Console.WriteLine("Entered body!");
				} else if (isIn && !engagedVal && value) {
					SetCollisionLayerBit(2, false);
					if (IsInGroup("prime_moving")) {
						RemoveFromGroup("prime_moving");
					}
					Console.WriteLine("Exited body!");
				}
				engagedVal = value;
			}
		}

		public void onBodyEntered (Node node) {
			isIn = true;
			if (engaged) {
				SetCollisionLayerBit(2, true);
				AddToGroup("prime_moving");
				Console.WriteLine("Entered body!");
			}
		}

		public void onBodyExited (Node node) {
			isIn = false;
			if (engaged) {
				SetCollisionLayerBit(2, false);
				if (IsInGroup("prime_moving")) {
					RemoveFromGroup("prime_moving");
				}

				Console.WriteLine("Exited body!");
			}
		}
		
	}
}