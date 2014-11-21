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
            this IEnumerable<T> toAdapt)
        {
            return new EnumerableToObservableAdapter<T>(toAdapt);
        }
    }
    public class NullableDisposable : IDisposable
    {
        public void Dispose()
        {
            return;
        }
    }

    public class EnumerableToObservableAdapter<T> : IObservable<T>
    {
        private readonly IEnumerable<T> _toAdapt;

        public EnumerableToObservableAdapter(IEnumerable<T> toAdapt )
        {
            _toAdapt = toAdapt;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            try
            {
                foreach (T item in _toAdapt)
                {
                    observer.OnNext(item);
                }
            }
            catch (Exception e)
            {
                observer.OnError(e);
            }

            observer.OnCompleted();

            return new NullableDisposable();
        }
    }

    public class DelegatingObserver<T> : IObserver<T>
    {
        private readonly Action<T> _onNext;
        private readonly Action<Exception> _onError;
        private readonly Action _onCompleted;

        public DelegatingObserver(Action<T> onNext, Action onCompleted) :
            this(onNext, _ => { }, onCompleted)
        {
            
        }

        public DelegatingObserver(Action<T> onNext) :
            this(onNext, _ => { }, () => { })
        {

        }

        public DelegatingObserver(Action<T> onNext, Action<Exception> onError, 
            Action onCompleted)
        {
            _onNext = onNext;
            _onError = onError;
            _onCompleted = onCompleted;
        }

        public void OnNext(T value)
        {
            _onNext(value);
        }

        public void OnError(Exception error)
        {
            _onError(error);
        }

        public void OnCompleted()
        {
            _onCompleted();
        }
    }

    public class Stalker : IObserver<Person>
    {
        public void OnNext(Person value)
        {
            Console.WriteLine("Watching {0}",value);
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Error {0}",error.Message);
        }

        public void OnCompleted()
        {
            Console.WriteLine("All done");
        }
    }

    class Program
    {
        private static void Main()
        {
            var people = 
                new List<Person>()
                    {
                        new Person{ Name = "Rich", Age=49},
                        new Person{ Name = "Andy", Age=43},
                        new Person{ Name = "Dave", Age=45},
                        new Person{ Name = "Kevin", Age=51},
                        new Person{ Name = "Simon", Age=52},
                    };


           // var observablePeople = new EnumerableToObservableAdapter<Person>(people);

            var stalker = 
                //new Stalker();
                new DelegatingObserver<Person>(Console.WriteLine);

            people
                .ToObservable()
                .Subscribe(stalker);


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
