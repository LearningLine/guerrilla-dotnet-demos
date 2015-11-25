using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class DirectoryViewModel
    {
        private FileSystemWatcher watcher;
        private SynchronizationContext ctx = SynchronizationContext.Current;

        public DirectoryViewModel(string directory)
        {
            Files = new ObservableCollection<string>(new DirectoryInfo(directory).GetFiles().Select(fi => fi.Name));

            watcher = new FileSystemWatcher(directory);
            watcher.Created += Created;
            watcher.Deleted += Deleted;

            watcher.EnableRaisingEvents = true;

            
          
        }

      

        private void Deleted(object sender, FileSystemEventArgs e)
        {
            ctx.Post(_ => Files.Remove(e.Name), null);
        }

        private void Created(object sender, FileSystemEventArgs e)
        {
            ctx.Post(_ => Files.Add(e.Name),null);
        }

        public ObservableCollection<string> Files { get; set; }
    }
}