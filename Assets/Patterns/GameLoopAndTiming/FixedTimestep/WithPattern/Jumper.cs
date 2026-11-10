using UnityEngine;

namespace GameDevPatterns.Patterns.FixedTimestep.WithPattern
{
	// The same hand-simulated jump as WithoutPattern, but advanced by a FixedStepRunner:
	// every step is 1/50 s whatever the frame rate, so every jump takes the same steps
	// and reaches the same peak. Rendering blends the last two states by the runner's alpha.
	public class Jumper : MonoBehaviour
	{
		[SerializeField] private float _jumpSpeed = 8f;
		[SerializeField] private float _gravity = 20f;
		[SerializeField] private float _secondsBetweenJumps = 1.5f;
		[SerializeField] private float _stepSeconds = 1f / 50f;
		[SerializeField] private bool _interpolate = true;

		private FixedStepRunner _runner;
		private float _groundY;
		private float _previousHeight;
		private float _height;          // above the ground
		private float _velocity;
		private float _restTime;
		private float _peak;

		// Peak height of the last finished jump, for the on-screen panel.
		public float LastPeak { get; private set; }

		// Turn off to see the stutter the page warns about when the frame rate isn't a multiple of the step rate.
		public bool Interpolate
		{
			get => this._interpolate;
			set => this._interpolate = value;
		}

		private void Start()
		{
			this._groundY = transform.position.y;
			this._runner = new FixedStepRunner(this._stepSeconds, Simulate);
		}

		private void Update()
		{
			float alpha = (float)this._runner.Advance(Time.deltaTime);
			float shown = this._interpolate ? Mathf.Lerp(this._previousHeight, this._height, alpha) : this._height;

			Vector3 position = transform.position;
			position.y = this._groundY + shown;
			transform.position = position;
		}

		// One constant-size step. Same rules as WithoutPattern's Update, with step instead of deltaTime.
		private void Simulate(double stepSeconds)
		{
			float dt = (float)stepSeconds;
			this._previousHeight = this._height;
			bool grounded = this._height <= 0f && this._velocity <= 0f;

			if (grounded)
			{
				this._restTime += dt;
				if (this._restTime >= this._secondsBetweenJumps)
				{
					this._restTime = 0f;
					this._peak = 0f;
					this._velocity = this._jumpSpeed;
				}
			}
			else
			{
				this._velocity -= this._gravity * dt;
				this._height += this._velocity * dt;
				this._peak = Mathf.Max(this._peak, this._height);

				if (this._height <= 0f)
				{
					this._height = 0f;
					this._velocity = 0f;
					this.LastPeak = this._peak;
				}
			}
		}
	}
}
