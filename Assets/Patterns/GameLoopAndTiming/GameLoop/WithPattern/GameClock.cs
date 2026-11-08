namespace GameDevPatterns.Patterns.GameLoop.WithPattern
{
	public static class GameClock
	{
		public static float GameTime { get; private set; }
		public static int TickCount { get; private set; }

		public static void Tick(float deltaTime)
		{
			GameTime += deltaTime;
			TickCount++;
		}

		public static void Reset()
		{
			GameTime = 0f;
			TickCount = 0;
		}
	}
}