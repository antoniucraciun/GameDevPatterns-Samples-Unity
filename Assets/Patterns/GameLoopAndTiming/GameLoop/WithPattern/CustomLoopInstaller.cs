using UnityEngine;

namespace GameDevPatterns.Patterns.GameLoop.WithPattern
{
    public class CustomLoopInstaller : MonoBehaviour
    {
		/// <summary>
		/// Unity Message: Called when the object becomes enabled and active. 
		/// This is where we install our custom loop. 
		/// A real game would do this once at startup with [RuntimeInitializeOnLoadMethod], as the docs page shows. 
		/// The sample installs it from its scene so it doesn’t run in other samples.
		/// </summary>
		private void OnEnable() => CustomLoop.Install();

		/// <summary>
		/// Unity Message: Called when the behaviour becomes disabled or inactive.
		/// </summary>
		private void OnDisable() => CustomLoop.Uninstall();
	}
}
