using UnityEngine;

namespace GameDevPatterns.Patterns.UpdateMethod.WithPattern
{
    public class SlidingDoor : MonoBehaviour
    {
		[SerializeField] private Vector3 _closedOffset = new Vector3(0f, -2f, 0f);
		[SerializeField] private float _seconds = 3f;

		private Vector3 _openPosition;
		private float _progress;          // 0 = open, 1 = closed
		private bool _closing = true;

		void Start()
        {
			this._openPosition = this.transform.position;
		}

        void Update()
        {
			this._progress += (this._closing ? Time.deltaTime : -Time.deltaTime) / this._seconds;

			if (this._progress >= 1f)
			{
				this._progress = 1f;
				this._closing = false;
			}
			else if (this._progress <= 0f)
			{
				this._progress = 0f;
				this._closing = true;
			}

			this.transform.position = Vector3.Lerp(this._openPosition, this._openPosition + this._closedOffset, this._progress);
		}
    }
}
