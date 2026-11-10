using UnityEngine;

namespace GameDevPatterns.Patterns.FixedTimestep.WithPattern
{
	// Scene glue: shows the numbers to compare and switches the frame rate.
	// Same panel as WithoutPattern, plus a toggle for the Jumper's interpolation.
	public class FixedTimestepHud : MonoBehaviour
	{
		[SerializeField] private Jumper _jumper;
		[SerializeField] private Rigidbody _rocket;
		[Range(0.1f, 5f)]
		[SerializeField] private float _guiScaleMultiplier = 2f;

		private readonly int[] _frameRateCaps = { 30, 60, 144, -1 };
		private readonly float _referenceHeight = 720f;
		private Vector3 _rocketStart;
		private float _rocketPeak;

		private void Start() => this._rocketStart = this._rocket.position;

		private void Update()
		{
			this._rocketPeak = Mathf.Max(this._rocketPeak, this._rocket.position.y - this._rocketStart.y);
		}

		private void OnGUI()
		{
			float scale = (Screen.height / this._referenceHeight) * this._guiScaleMultiplier;
			GUI.matrix = Matrix4x4.Scale(new Vector3(scale, scale, 1f));

			GUILayout.BeginArea(new Rect(10, 10, 300, 360), GUI.skin.box);
			GUILayout.Label(
				$"FPS: {1f / Time.unscaledDeltaTime:0}\n" +
				$"Jumper last peak: {this._jumper.LastPeak:0.000} m\n" +
				$"Rocket height: {this._rocket.position.y - this._rocketStart.y:0.00} m\n" +
				$"Rocket speed: {this._rocket.linearVelocity.y:0.00} m/s\n" +
				$"Rocket peak: {this._rocketPeak:0.00} m\n" +
				"Hold Space to fire the rocket.");

			foreach (int fps in this._frameRateCaps)
			{
				if (GUILayout.Button(fps < 0 ? "Uncapped" : $"{fps} FPS"))
				{
					QualitySettings.vSyncCount = 0;
					Application.targetFrameRate = fps;
				}
			}

			if (GUILayout.Button("Reset rocket"))
			{
				this._rocket.position = this._rocketStart;
				this._rocket.linearVelocity = Vector3.zero;
				this._rocketPeak = 0f;
			}

			this._jumper.Interpolate = GUILayout.Toggle(this._jumper.Interpolate, " Interpolate the jumper");

			GUILayout.EndArea();
		}
	}
}
