using System;
using System.Threading;
using System.Threading.Tasks;

namespace Helpers
{
	public static class Pseudo
	{
		private const int defaultSleepDuration = 1000;

		public static async Task LongRunningTaskAsync(int sleepDurationInMilliseconds = defaultSleepDuration)
		{
			await Task.Delay(sleepDurationInMilliseconds);
		}

		public static Task LongRunningCancellableTask(CancellationToken cancellationToken)
		{
			return Task.Run(() =>
				{
					int estimatedElapsedMilliseconds = 0;
					while (!cancellationToken.IsCancellationRequested || estimatedElapsedMilliseconds > 60000)
					{
						Thread.Sleep(1000);
						estimatedElapsedMilliseconds += 1000;
					}
				});
		}

		public static Action LongRunningAction(int sleepDurationInMilliseconds = defaultSleepDuration)
		{
			return new Action(() => Thread.Sleep(sleepDurationInMilliseconds));
		}

		public static Action<int> LongRunningActionInt(int sleepDurationInMilliseconds = defaultSleepDuration)
		{
			return new Action<int>((value) => Thread.Sleep(sleepDurationInMilliseconds));
		}
	}
}
