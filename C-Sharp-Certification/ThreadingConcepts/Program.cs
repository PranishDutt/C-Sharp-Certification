using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Helpers;

namespace ThreadingConcepts
{
	class Program
	{
		static void Main(string[] args)
		{
			// Setup
			int value = 0;
			const int oneBillion = 1000000000;

			// Without "lock" synchronization
			Console.WriteLine("Counting to 1,000,000,000 without locking...");

			ActionTimer.Time("Without \"lock\"", () =>
			{
				Task withoutLockTaskA = Task.Run(() =>
				{
					while (value < oneBillion)
					{
						value++;
					}
				});

				Task withoutLockTaskB = Task.Run(() =>
				{
					while (value < oneBillion)
					{
						Thread.Sleep(1);
						value++;
					}
				});

				Task.WaitAll(withoutLockTaskA, withoutLockTaskB);

			}, false);

			Console.WriteLine("Computed value: " + value.ToString("N0"));
			Console.WriteLine(value != oneBillion ? "(Incorrect value was produced due to race conditions)" : "(Correct value was produced... Try running it again)");
			Console.WriteLine();

			// Keyword: "lock" synchronization
			Console.WriteLine("Counting to 1,000,000,000 using keyword \"lock\" for synchronization...");
			value = 0;
			object keywordLockObject = new object();

			ActionTimer.Time("Keyword \"lock\" synchronization", () =>
			{
				Task withLockTaskA = Task.Run(() =>
				{
					while (true)
					{
						lock (keywordLockObject)
						{
							if (value < oneBillion)
								value++;
							else
								break;
						}
					}
				});

				Task withLockTaskB = Task.Run(() =>
				{
					while (true)
					{
						Thread.Sleep(1);

						lock (keywordLockObject)
						{
							if (value < oneBillion)
								value++;
							else
								break;
						}
					}
				});

				Task.WaitAll(withLockTaskA, withLockTaskB);

			}, false);

			Console.WriteLine("Computed value: " + value.ToString("N0"));
			Console.WriteLine(value != oneBillion ? "(Incorrect value was produced due to race conditions) [This message will never show]" : "(Correct value was produced)");
			Console.WriteLine();

			// Monitor equivalent of "lock" keyword
			Console.WriteLine("Counting to 1,000,000,000 using a monitor (lock equivalent) for synchronization.");
			value = 0;
			object monitorLockObject = new object();

			ActionTimer.Time("Monitor equivalent of \"lock\" keyword", () =>
			{
				Task withMonitorTaskA = Task.Run(() =>
				{
					while (true)
					{
						bool lockTaken = false;
						try
						{
							Monitor.Enter(monitorLockObject, ref lockTaken);

							if (value < oneBillion)
								value++;
							else
								break;
						}
						finally
						{
							if (lockTaken)
								Monitor.Exit(monitorLockObject);
						}

					}
				});

				Task withMonitorTaskB = Task.Run(() =>
				{
					while (true)
					{
						Thread.Sleep(1);

						bool lockTaken = false;
						try
						{
							Monitor.Enter(monitorLockObject, ref lockTaken);

							if (value < oneBillion)
								value++;
							else
								break;
						}
						finally
						{
							if (lockTaken)
								Monitor.Exit(monitorLockObject);
						}

					}
				});

				Task.WaitAll(withMonitorTaskA, withMonitorTaskB);

			});

			Console.WriteLine("Computed value: " + value.ToString("N0"));
			Console.WriteLine(value != oneBillion ? "(Incorrect value was produced due to race conditions) [This message will never show]" : "(Correct value was produced)");
			Console.WriteLine();

			Console.ReadLine();
		}
	}
}
