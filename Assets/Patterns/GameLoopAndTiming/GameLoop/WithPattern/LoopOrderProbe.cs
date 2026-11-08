using UnityEngine;

namespace GameDevPatterns.Patterns.GameLoop.WithPattern
{
	public class LoopOrderProbe : MonoBehaviour
	{
		[Range(0.1f, 5f)]
		[SerializeField] private float _guiScaleMultiplier = 2f;

		private int _fixedCalls;
		private int _updateCalls;
		private int _lastTickCount;
		private string _report = string.Empty;
		private float _startRealTime;

		private readonly int[] _frameRateCap = new int[] { 30, 60, 144, -1 };
		private readonly float _referenceHeight = 720f;

		private void Start()
		{
			this._startRealTime = Time.realtimeSinceStartup;
			this._lastTickCount = GameClock.TickCount;
		}

		private void FixedUpdate() => this._fixedCalls++;

		private void Update() => this._updateCalls++;

		private void LateUpdate()
		{
			int ticksThisFrame = GameClock.TickCount - this._lastTickCount;
			this._lastTickCount = GameClock.TickCount;

			this._report = $"FPS: {1.0f / Time.unscaledDeltaTime:0}\n" +
				$"Fixed Updates: {this._fixedCalls}\n" +
				$"Update Calls: {this._updateCalls}\n" +
				$"GameSystemUpdates: {ticksThisFrame}\n" +
				$"Real time: {Time.realtimeSinceStartup - this._startRealTime:0.00}s\n" +
				$"Game time: {GameClock.GameTime:0.00}s";

			this._fixedCalls = 0;
			this._updateCalls = 0;
		}

		private void OnGUI()
		{
			float scale = (Screen.height / this._referenceHeight) * this._guiScaleMultiplier;
			GUI.matrix = Matrix4x4.Scale(new Vector3(scale, scale, 1f));

			GUILayout.BeginArea(new Rect(10, 10, 360, 300), GUI.skin.box);
			GUILayout.Label(this._report);
			foreach (var fps in this._frameRateCap)
			{
				if (GUILayout.Button(fps < 0 ? "Uncapped" : $"{fps} FPS"))
				{
					QualitySettings.vSyncCount = 0;
					Application.targetFrameRate = fps;
				}
			}

			GUILayout.EndArea();
		}
	}
}