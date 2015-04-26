using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpers
{
	public static class Pseudo
	{
		#region Constants
		private const int defaultSleepDuration = 1000;

		#endregion

		#region Data Class
		public class StateCityData
		{
			public string State { get; set; }
			public string City { get; set; }

			public StateCityData(string state, string city)
			{
				this.State = state;
				this.City = city;
			}
		}

		#endregion

		#region Public API
		public static List<StateCityData> GetPseudoStateCityData()
		{
			return new List<StateCityData>()
			{
				new StateCityData("Texas", "Austin"),
				new StateCityData("New York", "New York"),
				new StateCityData("California", "San Diego"),
				new StateCityData("Massachusetts", "Boston"),
				new StateCityData("Tennessee", "Nashvile"),
				new StateCityData("Kansas", "Wichita"),
				new StateCityData("Nevada", "Las Vegas"),
				new StateCityData("Florida", "Hollywood"),
				new StateCityData("Pennsylvania", "Pittsburgh")
			};
		}

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

		#endregion
	}
}
