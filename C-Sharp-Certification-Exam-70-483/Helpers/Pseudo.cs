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
