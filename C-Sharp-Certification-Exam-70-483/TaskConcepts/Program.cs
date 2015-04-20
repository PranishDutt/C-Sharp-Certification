using System;
using System.Threading;
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
			Console.WriteLine(Environment.NewLine + "Disclaimer:" + Environment.NewLine + "TaskFactory.StartNew() runs faster because Task.Run goes through a setup process for async/await. The task returned by TaskFactory.StartNew can not be used with async/await unless unwraped into an asynchronous operation with TaskExtensions.Unwrap()." + Environment.NewLine);

			// Task Cancellation
			ActionTimer.Time("Task Cancellation", () =>
				{
					Console.WriteLine("Starting cancellable task which will run for 60 seconds if not cancelled (cancellation is set to occur after 3 seconds).");
					CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
					Task cancelableTask = Pseudo.LongRunningCancellableTask(cancellationTokenSource.Token);
					cancellationTokenSource.CancelAfter(3000);
					cancelableTask.Wait();
				});

			// Async / Await
			AsyncAwaitConcepts();

			Console.ReadLine();
		}

		private static async void AsyncAwaitConcepts()
		{
			// IMPORTANT NOTE: "async void" usage here is justified for example purposes only! "async" should not be used with void-returning methods except for UI callbacks.

			// Async / Await
			Console.WriteLine(Environment.NewLine + "Async / Await starting... Typing to the console will not be blocked.");
			await Pseudo.LongRunningTaskAsync(5000).ContinueWith((finishedTask) => Console.WriteLine(Environment.NewLine + "Async / Await finished without blocking."));

			// Unwrapping Tasks
			Console.WriteLine(Environment.NewLine + "Unwrapping starting...");
			// NOTE: Tasks started with Task.Factory.StartNew need to be unwrapped due to returning a Task<Task> for each subsequent invocation.
			await Countdown(3).ContinueWith(t => Countdown(t.Result)).Unwrap().ContinueWith(t => Countdown(t.Result)).Unwrap().ContinueWith(t => Countdown(t.Result)).Unwrap();
			Console.WriteLine(Environment.NewLine + "Unwrapping finished.");

			// Non-Unwrapped Task Example
			Console.WriteLine(Environment.NewLine + "Starting non-unwrapped example (note the subsequent Result.Result calls in the code and the possible concurrency issues at runtime with awaiting Task.Factory.StartNew):");
			await Countdown(3).ContinueWith(t => Countdown(t.Result)).ContinueWith(t => Countdown(t.Result.Result));
			Console.WriteLine(Environment.NewLine + "Non-unwrapped example finished with possible concurrency issues for awaiting Task.Factory.StartNew without unwrapping the tasks.");
		}

		static Task<int> Countdown(int n)
		{
			return Task<int>.Factory.StartNew((oldValue) =>
				{
					Pseudo.LongRunningAction().Invoke();
					Console.WriteLine(Environment.NewLine + ": " + (int)oldValue);
					return (int)oldValue - 1;
				}, n);
		}
	}
}
