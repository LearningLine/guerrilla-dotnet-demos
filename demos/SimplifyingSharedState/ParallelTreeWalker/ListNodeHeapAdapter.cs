using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ParallelTreeWalker
{
    public class BagNodeHeap<T> : INodeHeap<T>
    {
        ConcurrentBag<ITreeNode<T>> bag = new ConcurrentBag<ITreeNode<T>>();
        public void Add(ITreeNode<T> node)
        {
            bag.Add(node);
        }

        public bool TryTake(out ITreeNode<T> node)
        {
            return bag.TryTake(out node);
        }
    }

    public class ListNodeHeapAdapter<T> : INodeHeap<T>
    {
        private readonly List<ITreeNode<T>> toAdapt;

        public ListNodeHeapAdapter(List<ITreeNode<T>> toAdapt)
        {
            this.toAdapt = toAdapt;
        }

        public void Add(ITreeNode<T> node)
        {
            lock(toAdapt)
                toAdapt.Add(node);
        }

        public bool TryTake(out ITreeNode<T> node)
        {
            node = null;
            lock (toAdapt)
            {
                if (toAdapt.Count == 0) return false;
                node = toAdapt[0];
                toAdapt.RemoveAt(0);
                return true;
            }
       
        }
    }

   
}