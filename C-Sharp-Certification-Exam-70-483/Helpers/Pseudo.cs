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

		#region Data Classes
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

		public class CityData
		{
			public long CityId { get; set; }
			public long StateId { get; set; }
			public string City { get; set; }

			public CityData(long cityId, long stateId, string city)
			{
				this.CityId = cityId;
				this.StateId = stateId;
				this.City = city;
			}
		}

		public class StateData
		{
			public long StateId { get; set; }
			public string State { get; set; }

			public StateData(long StateId, string State)
			{
				this.StateId = StateId;
				this.State = State;
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

		public static List<CityData> GetPseudoCityData()
		{
			return new List<CityData>()
			{
				new CityData(1, 1, "Austin"),
				new CityData(2, 2, "New York"),
				new CityData(3, 3, "San Diego"),
				new CityData(4, 4, "Boston"),
				new CityData(5, 5, "Nashvile"),
				new CityData(6, 6, "Wichita"),
				new CityData(7, 7, "Las Vegas"),
				new CityData(8, 8, "Hollywood"),
				new CityData(9, 9, "Pittsburgh")
			};
		}

		public static List<StateData> GetPseudoStateData()
		{
			return new List<StateData>()
			{
				new StateData(7, "Nevada"),
				new StateData(2, "New York"),
				new StateData(8, "Florida"),
				new StateData(4, "Massachusetts"),
				new StateData(9, "Pennsylvania"),
				new StateData(5, "Tennessee"),
				new StateData(3, "California"),
				new StateData(1, "Texas"),
				new StateData(6, "Kansas")
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
