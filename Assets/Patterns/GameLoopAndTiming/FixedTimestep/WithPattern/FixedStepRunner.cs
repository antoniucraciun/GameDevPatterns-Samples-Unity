using System;

namespace GameDevPatterns.Patterns.FixedTimestep.WithPattern
{
	// Plain C#: call Advance from Update with Time.deltaTime.
	public class FixedStepRunner
	{
		private readonly double _step;
		private readonly Action<double> simulate;
		private double _accumulator;

		public FixedStepRunner(double stepSeconds, Action<double> simulate)
		{
			this._step = stepSeconds;
			this.simulate = simulate;
		}

		// Returns the interpolation alpha for rendering between the last two states.
		public double Advance(double frameTime)
		{
			this._accumulator += Math.Min(frameTime, 0.25);   // clamp after a hitch to avoid a spiral of death
			while (this._accumulator >= this._step)
			{
				this.simulate(this._step);
				this._accumulator -= this._step;
			}

			return this._accumulator / this._step;
		}
	}
}
