using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingConcepts
{
	class Program
	{
		static void Main(string[] args)
		{
			// Setup
			int value = 0;
			const int oneBillion = 1000000000;

			// Without "lock"
			Console.WriteLine("Counting to 1,000,000,000 without locking...");
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
			Console.WriteLine("Computed value: " + value.ToString("N0"));
			Console.WriteLine(value != oneBillion ? "(Incorrect value was produced due to race conditions)" : "(Correct value was produced... Try running it again.)");

			// Keyword: "lock"


			// Monitor Equivalent of "lock" keyword

			Console.ReadLine();
		}
	}
}
