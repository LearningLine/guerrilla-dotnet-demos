namespace ThreadSafety
{
    internal interface ICounter
    {
        int Value { get; }

        void Increment();
    }
}