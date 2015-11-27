using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Basics
{
    public static class ObservableExtensions
    {
        public static IObservable<T> ToObservable<T>(
            this IEnumerable<T> source)
        {
            return new EnumerableToObservableAdapter<T>(source);
        }

        public static IDisposable Subscribe<T>(
            this IObservable<T> subject, Action<T> onNext)
        {
            return subject.Subscribe(new DelegatingObserver<T>(onNext));
        }


    }
    public class NullDisposable : IDisposable
    {
        public static readonly NullDisposable Null = 
            new NullDisposable();

        private NullDisposable()
        {
            
        }

        public void Dispose()
        {
            return;
        }
    }
    public class EnumerableToObservableAdapter<T> : IObservable<T>
    {
        private readonly IEnumerable<T> toAdapt;

        public EnumerableToObservableAdapter(IEnumerable<T> toAdapt )
        {
            this.toAdapt = toAdapt;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            foreach (var item in toAdapt)
            {
                observer.OnNext(item);
            }
            observer.OnCompleted();

            return NullDisposable.Null;
        }
    }

    public class Stalker : IObserver<Person>
    {
        public void OnNext(Person value)
        {
            Console.WriteLine(value);
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error.Message);
        }

        public void OnCompleted()
        {
            Console.WriteLine("All done");
        }
    }

    public class DelegatingObserver<T> : IObserver<T>
    {
        private readonly Action<T> onNext;
        private readonly Action<Exception> onError;
        private readonly Action onCompleted;

        public DelegatingObserver(Action<T> onNext) :
            this(onNext, () => { })
        {
            
        }
        public DelegatingObserver(Action<T> onNext,
            Action onCompleted) :
                this(onNext, _ => { },onCompleted)
        {
            
        }
        public DelegatingObserver(
            Action<T> onNext ,
            Action<Exception> onError,
            Action onCompleted)
        {
            this.onNext = onNext;
            this.onError = onError;
            this.onCompleted = onCompleted;
        }

        public void OnNext(T value)
        {
            onNext(value);
        }

        public void OnError(Exception error)
        {
            onError(error);
        }

        public void OnCompleted()
        {
            onCompleted();
        }
    }

    class Program
    {
        private static void Main()
        {
            var people = 
                new List<Person>()
                    {
                        new Person{ Name = "Rich", Age=50},
                        new Person{ Name = "Andy", Age=44},
                        new Person{ Name = "Dave", Age=46},
                        new Person{ Name = "Kevin", Age=52},
                        new Person{ Name = "Simon", Age=53},
                    };

            //IObservable<Person> toWatch = people.ToObservable();

            //toWatch
            //    .Subscribe(new DelegatingObserver<Person>(Console.WriteLine));

            people
                .ToObservable()
                .Subscribe(Console.WriteLine);
        }
    }

 
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return string.Format("{0} is {1}", Name, Age);
        } 
    }
}
