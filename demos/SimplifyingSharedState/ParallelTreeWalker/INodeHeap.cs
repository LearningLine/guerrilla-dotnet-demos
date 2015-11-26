namespace ParallelTreeWalker
{
    public interface INodeHeap<T>
    {
        void Add(ITreeNode<T> node);
        bool TryTake(out ITreeNode<T> node);
    }
}