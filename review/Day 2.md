Day 2 Review
==============

Tasks
--------------------

Tasks can run on threads / but are not necessarily tied to threads
TaskScheduler controls mapping to threads

* ThreadPool
* Syn context scheduler

	var t = new Task(
		/* Some bit of code to run async here */	
		() => Console.WriteLine("Super asyncy line)
	);
	t.Start();

Or one shot:

	// var t = Task.Factory.StartNew(...
	var t = Task.Run(...); 


	// speed is captured via clouse
	int speed = 7;
	var t = Task.Run(_ => Run(speed) );

	var answer = t.???????????? // Requires another type of task

	var t2 = Task<string>.Run( ... );
	answer = t2.Result; // answer or block  

	if (t2.Wait(1000)) {
		answer = t2.Result;
	}


	var t = Task.Run( _ => throw new Exception("Boo!"));
    t.Wait(); // See ex here
    t.Result; // See ex here
    t.Exception != null; // See ex here
	
	.NET 4 - unobserved kills your app
	.NET 4.5 - ignored

States:

* Ran to completion
* Faulted
* Cancelled

Cancelled via CancellationTokenSource -> .Token

	src = new CancellationTokenSource();
	
	var t2 = Task<string>.Run( _ => {
	
		while(True) {
			// do work
			src.Token.ThrowIfCancellationRequested();
		}
		
		}
	);

Parent / child tasks:

Parent isn't complete until all children tasks completed.

* Created via parent
* Attached

	t2 = t1.ContinueWith(tPrev => tPrev.Result... );
	t2.Wait();

Should tasks always be based on CPU threads?
Not when they are IO driven...


Thread Safety
--------------------

Action required when...

* Shared state
* Updated non-atomically

	// is this atomic?
	i++; // no

Scale of sync options

1. Do nothing (errors)
2. Interlocked.* (Increment, Add, etc.)
3. lock (really uses Monitor.Enter / Exit)
	1. lock cannot have a timeout
	2. mutual exclusion
2. ReaderWriterLock(Slim)
	1. Better when many readers, few writers
2. Mutex (allows kernel level sync, typically cross process)
3. Semephore

Beware of the order (dining philosophers problem)

Maybe we could avoid all of this...

Just don't share state (copy it or something)
A class of "lock free" algorithms


Repository Pattern
--------------------

Goal: Prevent tight coupling to data access layer for:

* Ability to change db access tech
* Test with "fake" data

Key concept:

Aggregate Root

	       Order
	         |
	       ---------
	      |        |
	   OrderItem  ShippingHistory   

Just build an OrderRepository (not OrderItem for example)

Build commonality into base classes / interfaces

interface IRepository<T> / class EfRepository<T>

specialized:
	
	IOrderRepository : IRepository<Order> 
	{
	    IQueryable<Order> GetOrdersByDay(DateTime day);
	}


`IQueryable<Order>` allows CONSUMER to control the query.

`IEnumerable<Order>` allows DATA LAYER to control the query.

* IRepository<T> maps to DbSet<T> in EF
* IUnitOfWork maps to DbContext in EF

IUnitOfWork : IDisposible
{
	// think private DbContext ctx...

    IRepositoryA ...
	IRepositoryB ...
	IRepositoryC ...

	Commit();
}


REST
--------------------

Flips from RPC / SOAP to

* Uris - https://theserver.com/users/1443
* HTTP verbs - GET / POST / PUT / DELETE
* Status codes - 200: OK, 201: Created, 404: Not Found, 418: I'm a Tea Pot

This is the "architecture of the web". The web is the largest scale "system" ever built.

Model this via nouns, not actions & verbs.

In .NET this is built via ASP.NET Web API:

	class BooksController : ApiController
	{
		[Route("books")]
		[HttpGet()]
		IHttpActionResult Get() 
		{
		}

		[Route("books/{bookId}")]
		[HttpGet()]
		IHttpActionResult Get(int bookId) 
		{
		}
	}













