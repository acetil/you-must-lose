using System;
using Godot;

namespace youmustlose.level_logic {
	public class Level7Label : RichTextLabel {
		public float timeToRamFinish = 100;
		
		[Export] public float updateTime = 0.5f;
		[Export] public float cpuMean = 5;
		[Export] public float cpuMin = 0.5f;
		[Export] public float cpuMax = 10;
		[Export] public float cpuRes = 0.1f;
		[Export] public float tempMean = 60;
		[Export] public float tempMin = 40;
		[Export] public float tempMax = 80;
		[Export] public float tempRes = 1;
		[Export] public float gpuMean = 20;
		[Export] public float gpuMin = 5;
		[Export] public float gpuMax = 60;
		[Export] public float gpuRes = 1;

		[Export] public int maxUpdates = 4;
		[Export] public int ramStart = 10;

		private float cpu;
		private float temp;
		private float gpu;
		private float ram;

		private Random random = new Random();

		private bool updating = true;

		public override void _Ready () {
			cpu = cpuMean;
			temp = tempMean;
			gpu = gpuMean;
			ram = ramStart;
			var t1 = GetTree().CreateTimer(updateTime);
			var t2 = GetTree().CreateTimer(0.01f);
			t1.Connect("timeout", this, nameof(startTextUpdates));
			t2.Connect("timeout", this, nameof(startRamUpdates));
			updateStats();
		}

		private async void startTextUpdates () {
			while (updating) {
				updateStats();
				await ToSignal(GetTree().CreateTimer(updateTime), "timeout");
			}
		}

		private async void startRamUpdates () {
			var ramUpdateTime = timeToRamFinish / (100 - ramStart + 1);
			while (updating) {
				await ToSignal(GetTree().CreateTimer(ramUpdateTime), "timeout");
				ram++;
				//Text = $"CPU: {cpu}% \n RAM: {ram}% \n Core Temp: {temp}C \n GPU: {gpu}%";
			}
		}

		private void updateStats () {
			cpu = updateStat((int)Math.Round(random.NextDouble()) * maxUpdates, cpu, cpuMin, cpuMax, cpuMean, cpuRes);
			temp = updateStat((int)Math.Round(random.NextDouble()) * maxUpdates, temp, tempMin, tempMax, tempMean, tempRes);
			gpu = updateStat((int)Math.Round(random.NextDouble()) * maxUpdates, gpu, gpuMin, gpuMax, gpuMean, gpuRes);
			Text = $"CPU:       {cpu:g3}% \nRAM:       {ram}% \nCore Temp: {temp}C \nGPU:       {gpu}%";
		}

		private float updateStat (int updates, float curr, float min, float max, float mean, float res) {
			for (var i = 0; i < updates; i++) {
				var v = random.NextDouble();
				if (curr <= mean) {
					if (v < (curr - min) / (mean - min) * 0.5) {
						curr -= res;
					} else {
						curr += res;
					}
				} else {
					if (v < (max - curr) / (max - mean) * 0.5) {
						curr += res;
					} else {
						curr -= res;
					}
				}
			}

			return curr;
		}
	}
}