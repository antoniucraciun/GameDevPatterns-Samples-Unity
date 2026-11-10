using UnityEngine;

namespace GameDevPatterns.Patterns.FixedTimestep.WithoutPattern
{
	// A jump simulated by hand (no Rigidbody), advanced by the frame's deltaTime.
	// It is frame-rate independent in the everyday sense, but each frame is one integration
	// step of a different size, so the peak height changes with the frame rate.
	public class Jumper : MonoBehaviour
	{
		[SerializeField] private float _jumpSpeed = 8f;
		[SerializeField] private float _gravity = 20f;
		[SerializeField] private float _secondsBetweenJumps = 1.5f;

		private float _groundY;
		private float _height;          // above the ground
		private float _velocity;
		private float _restTime;
		private float _peak;

		// Peak height of the last finished jump, for the on-screen panel.
		public float LastPeak { get; private set; }

		private void Start() => this._groundY = transform.position.y;

		private void Update()
		{
			float dt = Time.deltaTime;
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

			Vector3 position = transform.position;
			position.y = this._groundY + this._height;
			transform.position = position;
		}
	}
}
