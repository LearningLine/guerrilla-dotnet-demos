using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTreeWalker
{
    public class TreeWalker<T>
    {
        private readonly ITreeNode<T> root;
        
        private readonly int levelOfConcurrency;

        private readonly INodeHeap<T> nodes; 


        public TreeWalker(ITreeNode<T> root , INodeHeap<T> nodes  )
        {
            this.root = root;
            this.nodes = nodes;
        }

        public void Do(Action<T> action)
        {
            ITreeNode<T> treeNode = root;
            do
            {
                action(treeNode.Item);

                foreach (ITreeNode<T> child in treeNode.Children)
                {
                    nodes.Add(child);
                }

            } while (nodes.TryTake(out treeNode));
        }

        public Task DoAsync(int maxLevelOfConcurrency,Action<T> action )
        {
            return Task.Factory.StartNew(() =>
                                         InternalDoAsync(root, action, maxLevelOfConcurrency));
        }

        private void InternalDoAsync(ITreeNode<T> treeNode, Action<T> action, int maxLevelOfConcurrency)
        {
            do
            {
                action(treeNode.Item);

                foreach (ITreeNode<T> child in treeNode.Children)
                {
                    if (maxLevelOfConcurrency > 0)
                    {
                        maxLevelOfConcurrency--;

                        ITreeNode<T> localChild = child;

                        Task.Factory
                            .StartNew(() => InternalDoAsync(localChild, action, 0),
                                      TaskCreationOptions.AttachedToParent);
                    }
                    else
                    {
                        nodes.Add(child);
                    }
                }

            } while (nodes.TryTake(out treeNode));

            return;
        }

       
    }
}