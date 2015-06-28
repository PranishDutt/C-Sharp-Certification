using System;
using System.Diagnostics;

namespace Helpers
{
    public static class ActionTimer
    {
		public static void Time(string title, Action action)
		{
			Stopwatch timer = new Stopwatch();

			timer.Start();
			action.Invoke();
			timer.Stop();

			Console.WriteLine(Environment.NewLine + title + ":" + Environment.NewLine + timer.ElapsedMilliseconds + "ms");
		}
    }
}
