Day 1
======================

Topics
------------

* Language mechanics
* Entity Framework
* WPF / XAML
* Unit Test

Language mechanics
-------------------

Event keyword: private delegates with public subscription that is thread safe.

`foreach`: Things that are `IEnumerable`

Could build our own class to implement this but "Generator methods" are easier:

* `yield return`
* `yield break` 

Anonymous methods: Write small bits of code and pass it around without creating a separate method.

* `delegate(int x, int y) { // code here }`
* `(x, y) => x * y // lots of type inference here`
* `_ => Console.WriteLine("Ran")`

Compiler still writes the method but we don't have to manage it.

Using a "closure" results in creation of a class.

Extension methods

Can add "methods" to instances of types

	public static ClassName
	{
		public static void Where(this IEnumerable set) 
		{
		}
	}


new[] {1,2,3}.Where()...

C# 5 has new initializers (properties and list / collection)

new Person() {Name = "Andy", ... };
 

var d = new Dictionary<string, List<int>>();

var d2 = GetThing();


Entity Framework
-----------------------------

EF is an ORM for RDBMSes ;)

Take table data into / from objects (C#)

Three parts of EF

* Conceptual model
* Storage model
* Mapping layer

Styles:

From database
Code first

 *.tt files generate "pure C#" classes (POCO)

DbContext is the starting point
-> DbSet's for tables

N changed objects -> N db calls

Ways to navigate relationships:

* Lazy
* Eager
* Explicit

Code first

Create classes first, generate the database to match.

enable-migrations / add-migration / update-database

WPF / XAML
-----------------------------

XAML separates design & behaviour

Classes:

* Application
* Window

Windows have 1 content -> layout panels

* Canvas
* Grid
* StackPanel
* DockPanel
* UniformGrid
* WrapPanel

Info from panels to children via:

DependencyProperties

based on DependencyObject - GetValue / SetValue

* Attached Properties
* Value inheritance
	
	<Window>
	     <DockPanel>  <!-- Window.Content = new DockPanel(); -->
				<Slider DockPanel.Dock="Bottom" 
					 /> <!--
							var s = new Slider(); 
							DockPanel.Children.Add(s);
							DockPanel.SetDock(s, "Bottom");
							  -->
		 </DockPanel>
	</Window>


Something about unit testing
-----------------------

    "Testing is a good thing to do"
                    -- Andy Clymer

Too much code - a sure sign you're not loving your tests.

Mutant testing...

* Catching bugs early are cheaper
* Code can *safely* be refactored
* Forces loose coupling
* Faster (over time)
* Write just enough code
* Documentation

Style is: 

* Red / Green / Refactor
* AAA: Arrange / Act / Assert

























































