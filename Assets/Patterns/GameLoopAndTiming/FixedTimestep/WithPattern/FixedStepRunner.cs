using System;

namespace GameDevPatterns.Patterns.FixedTimestep.WithPattern
{
	// Plain C#: call Advance from Update with Time.deltaTime.
	public class FixedStepRunner
	{
		private readonly double _step;
		private readonly double _maxFrameTime;
		private readonly Action<double> simulate;
		private double _accumulator;

		// maxFrameTime: the longest frame accepted, so a hitch can't demand hundreds of catch-up steps.
		public FixedStepRunner(double stepSeconds, Action<double> simulate, double maxFrameTime = 0.25)
		{
			this._step = stepSeconds;
			this.simulate = simulate;
			this._maxFrameTime = maxFrameTime;
		}

		// Returns the interpolation alpha for rendering between the last two states.
		public double Advance(double frameTime)
		{
			this._accumulator += Math.Min(frameTime, this._maxFrameTime);   // clamp: no spiral of death
			while (this._accumulator >= this._step)
			{
				this.simulate(this._step);
				this._accumulator -= this._step;
			}

			return this._accumulator / this._step;
		}
	}
}
