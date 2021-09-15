using Godot;

namespace youmustlose {
	public class Menu : Control {
		[Signal]
		public delegate void StartGame ();
		private Control title;
		private Control credits;

		public override void _Ready () {
			title = GetNode<Control>("Title");
			credits = GetNode<Control>("Credits");
			RemoveChild(credits);
		}

		public void onStart () {
			EmitSignal(nameof(StartGame));
		}

		public void onCredits () {
			AddChild(credits);
			RemoveChild(title);
		}

		public void onBack () {
			AddChild(title);
			RemoveChild(credits);
		}
		
	}
}