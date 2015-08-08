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
			// ConcurrentBag
			ConcurrentBag<int> numbersBag = new ConcurrentBag<int>();
			Console.WriteLine("Concurrent Bag (Adding & Taking in parallel on multiple threads):");
			Parallel.Invoke(() => displayBag(numbersBag, 0, 30), () => displayBag(numbersBag, 30, 10), () => displayBag(numbersBag, 40, 10), () => displayBag(numbersBag, 50, 10), () => displayBag(numbersBag, 60, 10));
			Console.WriteLine(Environment.NewLine + "* Note that the Concurrent Bag may \"steal\" unprocessed values from other threads after their own thread's values have finished being processed. Notice how the first call to displayBag() adds 30 items, whereas all other displayBag() calls only add 10. As a result, the displayed values show that threads process all values on each thread's \"internal\" list of values, and then steal values from other threads." + Environment.NewLine);

			// ConcurrentDictionary
			ConcurrentDictionary<int, int> numbersDictionary = new ConcurrentDictionary<int, int>(); // Default comparer, concurrency level, and initial capacity.
			Console.WriteLine("Concurrent Dictionary (Adding & Accessing in parallel on multiple threads):");
			Parallel.Invoke(() => displayDictionary(numbersDictionary, 0, 30), () => displayDictionary(numbersDictionary, 30, 10), () => displayDictionary(numbersDictionary, 40, 10), () => displayDictionary(numbersDictionary, 50, 10), () => displayDictionary(numbersDictionary, 60, 10));
			Console.WriteLine();

			// ConcurrentQueue
			ConcurrentQueue<int> numbersQueue = new ConcurrentQueue<int>();
			Console.WriteLine("Concurrent Queue (Enqueuing & Dequeuing in parallel on multiple threads):");
			Parallel.Invoke(() => displayQueue(numbersQueue, 0, 30), () => displayQueue(numbersQueue, 30, 10), () => displayQueue(numbersQueue, 40, 10), () => displayQueue(numbersQueue, 50, 10), () => displayQueue(numbersQueue, 60, 10));
			Console.WriteLine();

			// ConcurrentStack
			ConcurrentStack<int> numbersStack = new ConcurrentStack<int>();
			Console.WriteLine("Concurrent Stack (Pushing & Popping in parallel on multiple threads):");
			Parallel.Invoke(() => displayStack(numbersStack, 0, 30), () => displayStack(numbersStack, 30, 10), () => displayStack(numbersStack, 40, 10), () => displayStack(numbersStack, 50, 10), () => displayStack(numbersStack, 60, 10));
			Console.WriteLine();

			// BlockingCollection [ For Producer-Consumer Patterns ]
			BlockingCollection<int> numbersBlockingCollection = new BlockingCollection<int>();
			Console.WriteLine("BlockingCollection (Multiple Producers, Single Consumer):");
			Task producerTaskA = GetAndStartProducerTask(numbersBlockingCollection, 0, 30);
			Task producerTaskB = GetAndStartProducerTask(numbersBlockingCollection, 30, 10);
			Task producerTaskC = GetAndStartProducerTask(numbersBlockingCollection, 40, 10);
			Task producerTaskD = GetAndStartProducerTask(numbersBlockingCollection, 50, 10);
			Task producerTaskE = GetAndStartProducerTask(numbersBlockingCollection, 60, 10);
			Task consumerTask = GetAndStartConsumerTask(numbersBlockingCollection);

			Task.WaitAll(producerTaskA, producerTaskB, producerTaskC, producerTaskD, producerTaskE);
			numbersBlockingCollection.CompleteAdding();

			Task.WaitAll(consumerTask);
			numbersBlockingCollection.Dispose();

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

		private static void displayDictionary(ConcurrentDictionary<int, int> numbersDictionary, int enumerableStartValue, int count)
		{
			StringBuilder message = new StringBuilder();

			Enumerable.Range(enumerableStartValue, count).ToList().ForEach(value => numbersDictionary.TryAdd(value, value));
			Thread.Sleep(100); // Force a context change

			int endOfRange = enumerableStartValue + count;
			for (int i = enumerableStartValue; i < endOfRange; i++)
			{
				int value = numbersDictionary[i];
				message.Append(value).Append(" ");
				Thread.Sleep(1); // Force a context change
			}

			Console.WriteLine("Thread ({0}) processed: {1}", Thread.CurrentThread.ManagedThreadId, message);
		}

		private static void displayQueue(ConcurrentQueue<int> numbersQueue, int enumerableStartValue, int count)
		{
			StringBuilder message = new StringBuilder();

			Enumerable.Range(enumerableStartValue, count).ToList().ForEach(value => numbersQueue.Enqueue(value));
			Thread.Sleep(100); // Force a context change

			int dequeuedValue;
			while (numbersQueue.TryDequeue(out dequeuedValue))
			{
				message.Append(dequeuedValue + " ");
				Thread.Sleep(1); // Force a context change
			}

			Console.WriteLine("Thread ({0}) processed: {1}", Thread.CurrentThread.ManagedThreadId, message);
		}

		private static void displayStack(ConcurrentStack<int> numbersStack, int enumerableStartValue, int count)
		{
			StringBuilder message = new StringBuilder();

			numbersStack.PushRange(Enumerable.Range(enumerableStartValue, count).ToArray());
			Thread.Sleep(100); // Force a context change

			int poppedValue;
			while (numbersStack.TryPop(out poppedValue))
			{
				message.Append(poppedValue + " ");
				Thread.Sleep(1); // Force a context change
			}

			Console.WriteLine("Thread ({0}) processed: {1}", Thread.CurrentThread.ManagedThreadId, message);
		}

		private static Task GetAndStartProducerTask(BlockingCollection<int> numbersBlockingCollection, int enumerableStartValue, int count)
		{
			return Task.Run(() =>
			{
				Enumerable.Range(enumerableStartValue, count).ToList().ForEach(value => numbersBlockingCollection.Add(value));
				Thread.Sleep(1); // Force a context change
			});
		}

		private static Task GetAndStartConsumerTask(BlockingCollection<int> numbersBlockingCollection)
		{
			return Task.Run(() =>
			{
				StringBuilder message = new StringBuilder();

				while (!numbersBlockingCollection.IsCompleted)
				{
					int value;
					if (numbersBlockingCollection.TryTake(out value))
						message.Append(value + " ");

					Thread.Sleep(1); // Force a context change
				}

				Console.WriteLine("Processed: " + message);
			});
		}
	}
}
