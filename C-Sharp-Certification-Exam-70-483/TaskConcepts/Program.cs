using System;
using System.Threading.Tasks;
using Helpers;

namespace TaskConcepts
{
	class Program
	{
		static void Main(string[] args)
		{
			const int sleepDurationInSeconds = 1000;

			// TaskFactory.StartNew() vs. Task.Run()
			ActionTimer.Time("TaskFactory.StartNew()", () =>
				{
					Task factoryTask = Task.Factory.StartNew(() => Task.Delay(sleepDurationInSeconds));
					Task.WaitAll(factoryTask);
				});

			ActionTimer.Time("Task.Run()", () =>
				{
					Task runTask = Task.Run(() => Task.Delay(sleepDurationInSeconds));
					Task.WaitAll(runTask);
				});

			// Disclaimer
			Console.WriteLine(Environment.NewLine + "Disclaimer:" + Environment.NewLine + "TaskFactory.StartNew() runs faster because Task.Run goes through a setup process for async/await. The task returned by TaskFactory.StartNew can not be used with async/await unless unwraped into an asynchronous operation with TaskExtensions.Unwrap().");

			Console.ReadLine();
		}
	}
}
