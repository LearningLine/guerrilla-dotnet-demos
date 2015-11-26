using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTreeWalker
{
    public interface ITreeNode<T>
    {
        IEnumerable<ITreeNode<T>> Children { get;  }

        T Item { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            INodeHeap<FileSystemInfo> heap = 
              // new ListNodeHeapAdapter<FileSystemInfo>(new List<ITreeNode<FileSystemInfo>>());
              new BagNodeHeap<FileSystemInfo>();

            var root = new FileInfoTreeNodeAdapter(new DirectoryInfo(@"C:\program files"));


            var parallelWalker = new TreeWalker<FileSystemInfo>(
                root,heap);
            
            long totalSize = 0;
            var start = DateTime.Now;
            parallelWalker.Do(fi => totalSize++);
            Console.WriteLine(DateTime.Now - start );

            start = DateTime.Now;
            parallelWalker
                .DoAsync(4, fi => totalSize++)
                .Wait();

             Console.WriteLine(DateTime.Now - start );

        }
    }
}
