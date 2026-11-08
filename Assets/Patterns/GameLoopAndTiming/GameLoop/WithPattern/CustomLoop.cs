using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace GameDevPatterns.Patterns.GameLoop.WithPattern
{
	public static class CustomLoop
	{
		/// <summary>
		/// A real game installs this once at startup with [RuntimeInitializeOnLoadMethod], as the docs page shows.
		/// The sample installs it from its scene so it doesn’t run in other samples.
		/// </summary>
		// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		public static void Install()
		{
			Uninstall();
			GameClock.Reset();

			var loop = PlayerLoop.GetCurrentPlayerLoop();
			for (int i = 0; i < loop.subSystemList.Length; i++)
			{
				if (loop.subSystemList[i].type != typeof(Update))
					continue;

				var update = loop.subSystemList[i];
				var systems = new List<PlayerLoopSystem>(update.subSystemList)
				{
					new PlayerLoopSystem
					{
						type = typeof(GameSystemsUpdate),
						updateDelegate = Tick
					}
				};

				update.subSystemList = systems.ToArray();
				loop.subSystemList[i] = update;
			}

			PlayerLoop.SetPlayerLoop(loop);
		}

		/// <summary>
		/// Used to remove the custom loop from the player loop. This is useful for testing, so that the sample cannot run in other samples.
		/// </summary>
		public static void Uninstall()
		{
			var loop = PlayerLoop.GetCurrentPlayerLoop();
			for (int i = 0; i < loop.subSystemList.Length; i++)
			{
				if (loop.subSystemList[i].type != typeof(Update))
					continue;

				var update = loop.subSystemList[i];
				var systems = update.subSystemList.ToList();
				systems.RemoveAll(s => s.type == typeof(GameSystemsUpdate));
				update.subSystemList = systems.ToArray();
				loop.subSystemList[i] = update;
			}

			PlayerLoop.SetPlayerLoop(loop);
		}

		static void Tick()
		{
			GameClock.Tick(Time.deltaTime);
		}
	}

	// An empty marker type. The PlayerLoop identifies each step by a type: this one names the step in the Profiler, and lets you find or remove it later.
	public struct GameSystemsUpdate { }
}