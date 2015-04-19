﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Helpers
{
	public static class Pseudo
	{
		private const int sleepDurationInMilliseconds = 1000;


		public static Action LongRunningTaskAction()
		{
			return new Action(() => Thread.Sleep(sleepDurationInMilliseconds));
		}

		public static Action<int> LongRunningTaskActionInt()
		{
			return new Action<int>((value) => Thread.Sleep(sleepDurationInMilliseconds));
		}
	}
}