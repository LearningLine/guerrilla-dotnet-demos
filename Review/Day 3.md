Tasks
===========================

Tasks represent an async piece of work.

How do we start tasks?

* Task.Factory.StartNew( delegate ... )
* t = new Task( delegate ... ); t.Start();

Where do tasks run?

(default) ThreadPool

Do tasks keep process alive? threadpool -> background threads

Can tasks run outside the thread pool?
Task.Factory.StartNew( delegate ..., TaskCreationOptions.LongRunning )

Threadpool threads are for sharing, so share them.

Long running threads will start immediately rather than be based on thread pool algorithms.

Tasks can return values via: Task<T> (e.g. Task<int>).

var t = new Task<int>( () => n * n );
Console.WriteLine(t.Result);

Tasks can be RanToCompletion, Faulted, or Cancelled.

You must catch or observe exceptions
-------------------

	var t = new Task<int>( () => n * n );
	try { 
		Console.WriteLine(t.Result);
	} catch (AggregateException ae) {	
	}

ae.Flatten().InnerExceptions...

	try { 
		Console.WriteLine(t.Result);
	} catch (AggregateException ae) {	
		ae.Handle(e => { return e is DbException; });
	}

.NET 4.5 swallows exceptions.

Cancelling a task
-------------------

var tcs = new TaskCancellationSource();

Task.Factory.StartNew( delegate, tcs.Token);

in task:
token.ThrowIfCancellationRequested();

Parent / Child and Continuations
---------------------------

	task1
	   |
	   |- new task(..., TaskCreationOptions.AttachToParent)
	   |- new task(..., TaskCreationOptions.AttachToParent)
	   |- new task(..., TaskCreationOptions.AttachToParent)

task1.Wait() // only need to wait on the parent

// Can deny attaching to parents

**Continuations**

	task2 = task1.ContinueWith(tPrev => {/* set the stage*/} )
	.ContinueWith(tPrev => {/* fan out*/} );
	.ContinueWith(tPrev => {/* more fan out */} );

**Old Skool** to new:

    var t = Task.Factory.FromAsync<T>(webRequest.BeginGetResult, webRequest.EndResult);


Thread Safety
===========================

Andy claims: *"Tasks are easy, threading is hard"*

Ideally, everything is immutable, scale is easy.
Most apps change their memory structures.

Types we can use for sync:

**Interlock.Increment and friends**

for single operations.

Multi-step coordination:

	// Not exception safe
	Monitor.Enter(thing)
	// Do multi-step work
	Monitor.Exit(thing)


	lock (thing) {
		// Do multi-step work
	}

Don't wait forever, use a timeout:

	// Use Andy's IDisposible version of lock
	Monitor.Enter(thing, timout)
	// Do multi-step work
	Monitor.Exit(thing)

Only one monitor is aquired at a time (can lead to deadlocks).

Fixed via deterministic lock order.

**Heavy read situations favor reader/writer locks**

Use ReaderWriterLockSlim (over ReaderWriterLock). More reader friendly.

Before you throw sync at the problem, think about whether you can restructure algorithms to avoid shared data.

**Large scale sync**

* Semaphores
* Mutex - can be cross-process (unlike monitor)
* (+ slims)
* Barriers
* CountdownEvents


Async / Await
===========================

Arrives in C# 5.

async keyword does nothing other than enable use of await keyword.

Enables synchronous programming model that is actually async.

compiler rewrites code with continuations...

	// avoid:
	public async void Method() ...

	// use:
	public async Task Method() ...
	public async Task<T> Method() ...

To impl an async method, you must await

	public async Task<int> Method() {
		// work
		int num = await SomeThingAsync();
		// more work
		return num / 42;
	} // returned task completes here

await coerses exception to 'natural' type.

**4 gotchas**

1. just async keyword doesn't make it async
2. always return the task (not void)
3. for someone who expects an sync call don't turn it into an async call
4. await exceptions only return the first error

Control return sync contx via .ConfigureAwait(false/true).

	public async Task<int> Method() {
		// work on UI thread
		int num = await SomeThingAsync(); // bg thread
		// more work on UI thread
		return num / 42;
	} // returned task completes here

vs

	public async Task<int> Method() {
		// work on UI thread
		int num = await SomeThingAsync()
			.ConfigureAwait(false); // bg thread
		// more work on BACKGROUND
		return num / 42;
	} // returned task completes here

Waiting for many things:

* await Task.WhenAll()
* await Task.WhenAny()




