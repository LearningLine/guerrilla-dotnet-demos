using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TraditionalAsync.ViewModels
{
    public static class TaskUtils
    {
        public static IEnumerable<Task<T>> InOrder<T>(this IEnumerable<Task<T>> tasks)
        {
            Task<T>[] tasksToComplete = tasks.ToArray();
            var taskCompSourcesToComplete = new TaskCompletionSource<T>[tasksToComplete.Length];

            int nextTaskIndex = -1;
            for (int i = 0; i < tasksToComplete.Length; i++)
            {
                taskCompSourcesToComplete[i] = new TaskCompletionSource<T>();
                tasksToComplete[i].ContinueWith(t =>
                {
                    // nextTaskIndex++;
                    var index = Interlocked.Increment(ref nextTaskIndex);
                    taskCompSourcesToComplete[index].SetResult(t.Result);
                });

            }

            return taskCompSourcesToComplete.Select(tcs => tcs.Task);
        }
    }
}