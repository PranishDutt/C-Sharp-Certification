using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Helpers;

namespace ConcurrentCollections
{
	class Program
	{
		static void Main(string[] args)
		{
			// Setup
			ConcurrentBag<int> numbersBag = new ConcurrentBag<int>();

			Console.WriteLine("Concurrent Bag (Adding & Taking in parallel on multiple threads):");
			Parallel.Invoke(() => displayBag(numbersBag, 0, 30), () => displayBag(numbersBag, 30, 10), () => displayBag(numbersBag, 40, 10), () => displayBag(numbersBag, 50, 10), () => displayBag(numbersBag, 60, 10));
			Console.WriteLine(Environment.NewLine + "* Note that the Concurrent Bag may \"steal\" unprocessed values from other threads after their own thread's values have finished being processed. Notice how the first call to displayBag() adds 30 items, whereas all other displayBag() calls only add 10. As a result, the displayed values show that threads process all values on each thread's \"internal\" list of values, and then steal values from other threads." + Environment.NewLine);
			Console.ReadLine();
		}

		private static void displayBag(ConcurrentBag<int> numbersBag, int enumerableStartValue, int count)
		{
			StringBuilder message = new StringBuilder();

			Enumerable.Range(enumerableStartValue, count).ToList().ForEach(value => numbersBag.Add(value));
			Thread.Sleep(100); // Force a context change

			int retrievedValue;
			while (numbersBag.TryTake(out retrievedValue))
			{
				message.Append(retrievedValue).Append(" ");
				Thread.Sleep(1); // Force a context change
			}

			Console.WriteLine("Thread ({0}) processed: {1}", Thread.CurrentThread.ManagedThreadId, message);
		}
	}
}
