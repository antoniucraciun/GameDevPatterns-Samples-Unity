using UnityEngine;

namespace GameDevPatterns.Patterns.FixedTimestep.WithoutPattern
{
	// The first version most people write: read the key and push the body, all in Update.
	// Unity adds up every AddForce call made between two physics steps and applies the sum
	// in the next step, so the push per second depends on how many frames you render:
	// about 0.6 pushes per 50 Hz physics step at 30 FPS, about 2.9 at 144 FPS.
	public class Thruster : MonoBehaviour
	{
		[SerializeField] private float _force = 20f;
		private Rigidbody _body;

		private void Awake() => this._body = GetComponent<Rigidbody>();

		private void Update()
		{
			if (Input.GetKey(KeyCode.Space))
				this._body.AddForce(Vector3.up * this._force);
		}
	}
}
