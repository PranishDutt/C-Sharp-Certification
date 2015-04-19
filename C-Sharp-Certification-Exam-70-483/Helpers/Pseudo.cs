using System;
using System.Threading;
using System.Threading.Tasks;

namespace Helpers
{
	public static class Pseudo
	{
		private const int sleepDurationInMilliseconds = 1000;


		public static Action LongRunningAction()
		{
			return new Action(() => Thread.Sleep(sleepDurationInMilliseconds));
		}

		public static Action<int> LongRunningActionInt()
		{
			return new Action<int>((value) => Thread.Sleep(sleepDurationInMilliseconds));
		}
	}
}
