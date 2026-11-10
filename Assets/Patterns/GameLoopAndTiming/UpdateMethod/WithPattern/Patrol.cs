using UnityEngine;

namespace GameDevPatterns.Patterns.UpdateMethod.WithPattern
{
	public class Patrol : MonoBehaviour
	{
		[SerializeField] Transform[] waypoints;
		[SerializeField] float speed = 3f;
		int index;

		void Update()
		{
			Vector3 target = this.waypoints[this.index].position;
			transform.position = Vector3.MoveTowards(transform.position, target, this.speed * Time.deltaTime);
			if (transform.position == target)
				this.index = (this.index + 1) % this.waypoints.Length;
		}
	}
}
