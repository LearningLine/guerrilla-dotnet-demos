using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParallelTreeWalker
{
    public class FileInfoTreeNodeAdapter : ITreeNode<FileSystemInfo>
    {
        public FileInfoTreeNodeAdapter(FileSystemInfo fileSystemInfo)
        {
            Item = fileSystemInfo;

        }

        public IEnumerable<ITreeNode<FileSystemInfo>> Children
        {
            get {
                var dir = Item as DirectoryInfo;
                if (dir == null) return Enumerable.Empty<ITreeNode<FileSystemInfo>>();

                return dir.GetFileSystemInfos()
                    .Select(fi => new FileInfoTreeNodeAdapter(fi));
            }
        }

        public FileSystemInfo Item { get; set; }
    }
}