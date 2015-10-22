using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BadLibs
{
    public class StoryEngine
    {
        private readonly Random _rand = new Random();

        public int DelayMs { get; set; }

        public async Task<IEnumerable<TextSection>> CreateStoryAsync(int storyId, CancellationToken cancelToken)
        {
            Debug.WriteLine("Now running on " + Thread.CurrentThread.ManagedThreadId);

            var sourceWordsTask = GetSourceWordsAsync();
            var storyFilesTask = ListStoryFilesAsync();
            IEnumerable<string> sourceWords = await sourceWordsTask.ConfigureAwait(false);
            IEnumerable<string> storyFiles = await storyFilesTask.ConfigureAwait(false);
            Debug.WriteLine("Now running on " + Thread.CurrentThread.ManagedThreadId);    

            cancelToken.ThrowIfCancellationRequested();

            IDictionary<string, IEnumerable<string>> allStories = await LoadAllStoriesAsync(storyFiles);

            cancelToken.ThrowIfCancellationRequested();

            IEnumerable<string> sections = null;
            try
            {
                sections = allStories.First(f => f.Key.Contains(String.Format("Story{0}.txt", storyId))).Value;
            }
            catch (Exception)
            {
                return new TextSection[0];
            }
            cancelToken.ThrowIfCancellationRequested();

            IEnumerable<TextSection> finalGroups = await ProcessStoryAsync(sourceWords, sections);
            cancelToken.ThrowIfCancellationRequested();

            return finalGroups;
        }

        private async Task<IDictionary<string, IEnumerable<string>>> LoadAllStoriesAsync(IEnumerable<string> storyFiles)
        {
            //var allStories = new Dictionary<string, IEnumerable<string>>();
            var allStories = new ConcurrentDictionary<string, IEnumerable<string>>();
            var tasks = new List<Task>();

            foreach (string file in storyFiles)
            {
                tasks.Add(GetStorySectionsAsync(file).ContinueWith(t => allStories.TryAdd(file, t.Result)));

            }
            await Task.WhenAll(tasks.ToArray());

            //Parallel.ForEach(storyFiles, file =>
            //{
            //    allStories.TryAdd(file, GetStorySections(file));
            //});
            return allStories;
        }

        private async Task<string[]> GetSourceWordsAsync()
        {
            await Task.Delay(DelayMs);
            return File.ReadAllLines(@"Data\dictionary.txt");
        }

        private async Task<IEnumerable<string>> ListStoryFilesAsync()
        {
            await Task.Delay(DelayMs);
            return Directory.GetFiles(@"Data\Stories\", "*.txt");
        }

        private async Task<IEnumerable<string>> GetStorySectionsAsync(string storyFile)
        {
            await Task.Delay(DelayMs);
            string filePath = storyFile;
            var storySource = File.ReadAllText(filePath);

            return storySource.Split(new[] { '^' }, StringSplitOptions.None);
        }

        private async Task<IEnumerable<TextSection>> ProcessStoryAsync(IEnumerable<string> sourceWords, IEnumerable<string> sections)
        {
            await Task.Delay(DelayMs);
            var finalGroups = new List<TextSection> { new TextSection(sections.First()) };

            foreach (string section in sections.Skip(1))
            {
                finalGroups.Add(new TextSection(sourceWords.ElementAt(_rand.Next(sourceWords.Count() - 1))) { Format = true });
                finalGroups.Add(new TextSection(section));
            }
            return finalGroups;
        }
    }
}