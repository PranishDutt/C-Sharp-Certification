using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Helpers;

namespace ParallelMethods
{
	class Program
	{
		static void Main(string[] args)
		{
			// Setup
			int iterations = 10;
			Console.WriteLine("Starting... ");

			// Non-Parallel
			ActionTimer.Time("Non-Parallel", () =>
				{
					for (int i = 0; i < iterations; i++)
					{
						Pseudo.LongRunningAction().Invoke();
					}
				});

			// Parallel.For
			ActionTimer.Time("Parallel.For", () => Parallel.For(0, iterations, Pseudo.LongRunningActionInt()));

			// Parallel.Foreach
			ActionTimer.Time("Parallel.ForEach", () => Parallel.ForEach(Enumerable.Range(0, iterations), Pseudo.LongRunningActionInt()));

			// Parallel.Invoke
			Action[] iterationActions = new Action[iterations];
			for (int i = 0; i < iterationActions.Length; i++)
			{
				iterationActions[i] = Pseudo.LongRunningAction();
			}

			ActionTimer.Time("Parallel.Invoke", () => Parallel.Invoke(iterationActions));

			// Other Parallel method overloads
			ActionTimer.Time("Parallel.Foreach overload using the TLocal variable and TLocal finalizer (time has significance for this example)", () =>
			{
				const int maxRange = 100000;
				int totalBytes = 0;
				Random random = new Random();
				Parallel.ForEach(Enumerable.Range(0, random.Next(maxRange / 2, maxRange)), () => 0,
					(integer, loopState, threadLocalSubtotal) =>
					{
						return threadLocalSubtotal + BitConverter.GetBytes(integer).Length;
					},
					(threadLocalSubtotal) =>
					{
						Interlocked.Add(ref totalBytes, threadLocalSubtotal);
					});
			});


			ActionTimer.Time("Parallel.Invoke", () => Parallel.Invoke(iterationActions));

			// P-LINQ Setup
			iterations = 100000;
			List<int> dataSet = Enumerable.Range(1, iterations).ToList();

			// LINQ (Non-Parallelized) Method Syntax
			ActionTimer.Time("LINQ (Non-Parallelized) Method Syntax", () => dataSet.Where(value => value % 2 == 0).ToList());

			// LINQ (Non-Parallelized) Query Syntax
			ActionTimer.Time("LINQ (Non-Parallelized) Query Syntax", () => 
				{
					(from value in dataSet
					 where value % 2 == 0
					 select value).ToList();
				});

			// P-LINQ Method Syntax
			ActionTimer.Time("P-LINQ Method Syntax", () => dataSet.AsParallel().Where(value => value % 2 == 0).ToList());

			// P-LINQ Query Syntax
			ActionTimer.Time("P-LINQ Query Syntax", () => 
					(from value in dataSet.AsParallel()
					 where value % 2 == 0
					 select value).ToList()
				);

			// LINQ ForEach
			ActionTimer.Time("LINQ ForEach", () => dataSet.ForEach((value) => { value = value / 2; }));

			// P-LINQ ForAll
			ActionTimer.Time("P-LINQ ForAll", () => dataSet.AsParallel().ForAll((value) => { value = value / 2; }));

			// Disclaimer
			Console.WriteLine(Environment.NewLine + "Disclaimer:" + Environment.NewLine + "Subsequent Parallel method calls may run faster due to memory caching.");

			Console.ReadLine();
		}
	}
}
