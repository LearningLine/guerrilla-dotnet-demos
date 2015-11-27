Day 4 Review
===================

(Easier) Thread Safety
------------

Want our code

* correct
* fast
* simple as possible


Andy's favorite type:

Lazy<T> - creates expensive object only when consumed

var l = Lazy<Repository>();
...
l.Value... <- Created here

Needs either:

* default constructor
* a delegate to create T

Default behaviour of Lazy<T> is creation is fully thread safe.


Collections:

* List<T>, ArrayList <-- not thread safe.
* ArrayList.Synchornize(new ArrayList()) <-- false sense of safety
* Concurrent collections

	// this is tricky
	if (list.Any())
	{
		list.Remove(0);
	}

	int item;
    if (queue.TryDequeue(ref item))
		// work with item

Build to model producer / consumer threads

* Add via `Add`
* Consumer / remove via `foreach( var item in blocker.GetConsumingEnumerable()) {}`
* producer: `block.CompleteAdding();`

Immutable collections:

NuGet package from VS team



Debugging
------------

* Exceptions
* Memory
* Threads

All based around memory --dumps-- snapshot ;).

* Visual Studio
* Task Manager
* DebugDiag (hands off, rule based)
* adplus

Types:

* mini dump -- not useful to .net debugging
* full dump

Launch windbg

.load sos
.loadby sos clr

Common commands:

!threads
!dumpheap -stat
!dumpheap -type Controller (all controller classes)
!printexception
~5e!pe
~5s (switch)
!clrstack -a (may require an *ini* file)
!do
!syncblock 
!dso (top passed to monitor.enter)
use sosex.dll !dlk

events are often the cause of memory leaks
!gcroot


Reactive Framework
------------

Based on `IObservable` / `IObserver`
Pushed base rather than pull based

Can adapt collections via `Observable`

Consumed via LINQ


SignalR
------------

Goal: Reverse the way the web works

The server calls the web browsers. Unifies

* Comet long polling
* forever iframes
* **web sockets**
* server-sent events

Primary for JavaScript clients, but .NET also exist.

	// startup.cs
	app.MapSignalR();
	
	class MyHub : Hub
	{
		public void SayHello(string name) {
			Clients.Other.greetingReceived(name + " says hello");
		}
	}

	// html
	<script src="/scripts/jquery-min.v.v.v.v.js"></script>
	<script src="/scripts/jquery.signalr.v.v.v.v.js"></script>
	<script src="/signalr/hubs"></script>

	// your.js
	$.connection.myHub.client.greetingReceived = function(name) {
	};
	$.connection.hub.start().done(function() {
		$.connection.myHub.server.sayHello("Jeff");
	}); 


Scale out via SignalR backbone.
































