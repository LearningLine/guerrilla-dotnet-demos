Day 3 review
=======================

Data Binding + MVVM
--------------------

Allows loosely coupled code for "Window" code

**Binding**:

* Source: POCO - any object
* Target: DependencyProp in the UI
* Path: Property from POCO

When is the binding *triggered*?

* Lost focus
* explicit
* on property changed

**Binding expressions**

var p = new Person();
this.DataContext = p;
<TextBox Text="{Binding Path=Name}" />
<TextBox Text="{Binding Name}" />
<TextBox Text="{Binding ElementName=slider Path=Value}" />
	
	IValueConverter 
	{
		ConvertFrom...
		ConvertTo
	}


	p.Name = "New Name";
	
	class Person : INotifyPropertyName
	{
		event OnPropertyChanged(...);
	
	}

**MVVM: Model View ViewModel**

	ViewModelBase : INotifyPropertyChanged
	{
	}
	
	CustomerViewModel : ViewModelBase
	{
	   properties...
	   commands... // typically a DelegateCommand
	}

**Data Templates**:

Convert regular objects into a UI representation.


Mocking
------------------

Unit testing *requires* isolating the subject under test.

**Frameworks**:

* moq
* rhinomocks
* etc.

**Continuum of doubles**:

* fakes - compile / run without crashing
* stubs - provides data to drive test
* spies - observes the actions within the SUT
* mocks - knows what to expect in addition to all the above

Remember to "Love your tests"


Async / await
-----------------

`async` keyword: allows us to use the await keyword
`await` keyword: asynchronously wait for some Task<T> operation

	public async Task<string> GetQuote()
	{
		try {
			List<Saying> sayings = await GetQuotesFromServerAsync()
													.ConfigureAwait(false);
			return sayings[0];
		}
		catch(NetworkException ne) {
			return "Nothing special to say";
		}
	}

async waits:

	string result = await Task.WhenAny(tasks);

GC
--------------------

"The Garbage Collector is your Mum" -- Andy

In .NET there are is crazy amount of allocation with temp variables, boxing, etc.

Garbage is found via: Finding things which are alive.

Follow the roots (stack refs, static variables)

* Find live objects
* Compact the heap

**Optimized via Generations**

Gen 0, 1, & 2

Gen 0 - new objects
Gen 1 - buffer between gen 0 & 2 for mid life objects

Gen 0 & 1 collections are really fast

Gen 2 - old objects, much slower
 

**Different styles of GC**:

concurrent
nonconcurrent
server

**Deterministic clean up via IDisposable**:

When to implement it? When you own IDisposable objects in your class.

Finalizer: Bad, you don't need them.

**How do you work with or aganist the GC**?

Bad:

* Reference heavy algorithms
* setting certain variables to null can be helpful (but not always)
