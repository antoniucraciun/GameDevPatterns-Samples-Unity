using UnityEngine;

namespace GameDevPatterns.Patterns.FixedTimestep.WithPattern
{
	// FixedUpdate runs at Time.fixedDeltaTime (Project Settings → Time).
	public class Thruster : MonoBehaviour
	{
		[SerializeField] float force = 20f;
		Rigidbody body;
		bool thrustHeld;

		void Awake() => body = GetComponent<Rigidbody>();

		// Input is sampled every rendered frame...
		void Update() => thrustHeld = Input.GetKey(KeyCode.Space);

		// ...but applied in constant-size simulation steps.
		void FixedUpdate()
		{
			if (thrustHeld)
				body.AddForce(Vector3.up * force);
		}
	}
}
