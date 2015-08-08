# C-Sharp-Certification (Microsoft Specialist - Programming in C#)
A collection of practice materials and examples that I have created in my preparation for the Microsoft "Programming In C# (70-483)" exam.

Topics Include:
--
* **Task Parallel Library**
  * **Parallel Methods**
    * **Parallel.For**
    * **Parallel.Foreach**
    * **Parallel.Invoke**
  * **P-LINQ**
    * **AsParallel()**
    * **ForAll()**
* **Tasks**
  * **TaskFactory.StartNew() vs. Task.Run()**
  * **All variations of "Wait"**
  * **Keywords: async/await**
    * **Interactions with TaskFactory.StartNew()**
      * **Task unwrapping**
* **Task Cancellation using CancellationTokens**
* **LINQ**
 * **Projection**
 * **Query Keywords:** https://msdn.microsoft.com/en-us/library/bb310804.aspx
 * **Query Expressions:** https://msdn.microsoft.com/en-us/library/bb669116%28v=vs.110%29.aspx
 * **Join, Group, Take, Skip, Aggregate**
 * **LINQ Extension Methods**
 * **Query Syntax vs. Method Syntax**
 * **Deffered Query Execution**
   * **Query is not actually executed until it is enumerated.**
* **Concurrent Collections:**
  * **ConcurrentBag**
  * **ConcurrentDictionary**
  * **ConcurrentQueue**
  * **ConcurrentStack**
  * **BlockingCollection**
* Threading:
  * Keyword: "lock"
  * AutoResetEvent
  * Barrier
  * Interlocked
  * LockCookie
  * ManualResetEvent
  * ManualResetEventSlim
  * Monitor
  * Mutex
  * ReaderWriterLock
  * ReaderWriterLockSlim
  * Sempahore
  * SemaphoreSlim
  * SpinLock
  * SpinWait
  * WaitHandle
* Keyword: "virtual"
* Keyword: "sealed"
* Keyword: "operator"
* Keyword: "volatile"
* Keyword: "unsafe"
* Keyword: "implicit"
* Keyword: "checked"
* Keyword: "unchecked"
* Delegates:
 * Func<T,U> | Action<T> | Comparison<T> | Comparison<T,U> | Predicate<T>
 * EventHandler<T>
  * Subscribe
  * Unsubscribe
   * Always unsubscribe when finished/object disposed/out of scope.
* Structs
* Enums & Casting / Setting the "enum value" by using inheritance syntax to specify a type.
* Generics
* Pass by Reference vs. Value
* Implementing IDisposable correctly
* IComparable, IEnumerable, IUnknown
* Polymorphism
 * Overload
 * Override
* Abstraction, Encapuslation, Inheritance, Composition
* Asymmetric / Symmetric encryption
* Hashing + Salting data
* System.Diagnostics.Trace
 * TraceListeners
* Profiling
* System.Diagnostics.EventLog
* File Methods:
 * File.ReadAllLines, File.ReadLine, File.WriteAllLines
* Streams:
 * CryptoStream
 * FileStream
 * MemoryStream
* System.Net
 * WebRequest, WebResponse, HttpWebRequest, HttpWebResponse
* LINQ to XML
 * XDocument.Load
 * XElement, XAttribute
* XmlReader, XmlTextReader, XmlWriter, XmlNavigator
* BinarySerialization, CustomSerialization, XMLSerilization, DataContractSerializer, DataContractJSONSerializer 
