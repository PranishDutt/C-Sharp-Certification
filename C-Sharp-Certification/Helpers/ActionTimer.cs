using System;
using System.Diagnostics;

namespace Helpers
{
    public static class ActionTimer
    {
		public static void Time(string title, Action action, bool prependNewLine = true)
		{
			Stopwatch timer = new Stopwatch();

			timer.Start();
			action.Invoke();
			timer.Stop();

			Console.WriteLine((prependNewLine ? Environment.NewLine : "") + title + ":" + Environment.NewLine + timer.ElapsedMilliseconds + "ms");
		}
    }
}
