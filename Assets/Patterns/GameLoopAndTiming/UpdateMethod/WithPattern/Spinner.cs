using UnityEngine;

namespace GameDevPatterns.Patterns.UpdateMethod.WithPattern
{
	public class Spinner : MonoBehaviour
	{
		[SerializeField] private float _degreesPerSecond = 90f;

		void Update()
		{
			transform.Rotate(0f, this._degreesPerSecond * Time.deltaTime, 0f);
		}
	}

}
