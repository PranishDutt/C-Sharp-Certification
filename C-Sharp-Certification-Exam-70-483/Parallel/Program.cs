using System;
using System.Linq;
using System.Collections;
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

			// Non-Parallel
			ActionTimer.Time("Non-Parallel", () =>
				{
					for (int i = 0; i < iterations; i++)
					{
						Pseudo.LongRunningTaskAction().Invoke();
					}
				});

			// Parallel.For
			ActionTimer.Time("Parallel.For", () => Parallel.For(0, iterations, Pseudo.LongRunningTaskActionInt()));

			// Parallel.Foreach
			ActionTimer.Time("Parallel.ForEach", () => Parallel.ForEach(Enumerable.Range(0, iterations), Pseudo.LongRunningTaskActionInt()));

			// Parallel.Invoke
			Action[] iterationActions = new Action[iterations];
			for (int i = 0; i < iterationActions.Length; i++)
			{
				iterationActions[i] = Pseudo.LongRunningTaskAction();
			}

			ActionTimer.Time("Parallel.Invoke", () => Parallel.Invoke(iterationActions));

			// Disclaimer
			Console.WriteLine(Environment.NewLine + "Disclaimer: Subsequent Parallel method calls may run faster due to memory caching.");

			Console.ReadLine();
		}


	}
}
