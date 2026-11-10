using UnityEngine;

namespace GameDevPatterns.Patterns.UpdateMethod.WithoutPattern
{
	public class WorldUpdater : MonoBehaviour
	{
		[Header("Guard")]
		[SerializeField] private Transform _guard;
		[SerializeField] private Transform[] _waypoints;
		[SerializeField] private float _guardSpeed = 3f;

		[Header("Pickup")]
		[SerializeField] private Transform _pickup;
		[SerializeField] private float _spinDegreesPerSecond = 90f;

		[Header("Door")]
		[SerializeField] private Transform _door;
		[SerializeField] private Vector3 _closedOffset = new Vector3(0f, -2f, 0f);
		[SerializeField] private float _doorSeconds = 3f;

		private int _guardWaypointIndex;
		private Vector3 _doorOpenPosition;
		private float _doorProgress;          // 0 = open, 1 = closed
		private bool _doorClosing = true;

		private void Start()
		{
			_doorOpenPosition = this._door.position;
		}

		void Update()
		{
			float dt = Time.deltaTime;

			// Guard: walk to the next waypoint, then loop.
			Vector3 target = this._waypoints[this._guardWaypointIndex].position;
			this._guard.position = Vector3.MoveTowards(this._guard.position, target, this._guardSpeed * dt);

			if (this._guard.position == target)
			{
				this._guardWaypointIndex = (this._guardWaypointIndex + 1) % this._waypoints.Length;
			}

			// Pickup: spin in place.
			this._pickup.Rotate(0f, this._spinDegreesPerSecond * dt, 0f);

			// Door: slide closed, then open again, forever.
			this._doorProgress += (this._doorClosing ? dt : -dt) / this._doorSeconds;

			if (this._doorProgress >= 1f) 
			{ 
				this._doorProgress = 1f;
				this._doorClosing = false;
			}
			else if (this._doorProgress <= 0f)
			{
				this._doorProgress = 0f;
				this._doorClosing = true;
			}

			this._door.position = Vector3.Lerp(this._doorOpenPosition, this._doorOpenPosition + this._closedOffset, this._doorProgress);

			// If you intend to add more objects to update, you will need to add more code here. This is a maintenance problem that the Update Method pattern solves.
		}
	}
}
