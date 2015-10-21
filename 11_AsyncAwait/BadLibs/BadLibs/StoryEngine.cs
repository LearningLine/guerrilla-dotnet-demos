using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

        public IEnumerable<TextSection> CreateStory(int storyId)
        {
            IEnumerable<string> sourceWords = GetSourceWords();

            IEnumerable<string> storyFiles = ListStoryFiles();

            IDictionary<string, IEnumerable<string>> allStories = LoadAllStories(storyFiles);

            IEnumerable<string> sections = allStories.First(f => f.Key.Contains(String.Format("Story{0}.txt", storyId))).Value;

            IEnumerable<TextSection> finalGroups = ProcessStory(sourceWords, sections);

            return finalGroups;
        }

        private IDictionary<string, IEnumerable<string>> LoadAllStories(IEnumerable<string> storyFiles)
        {
            var allStories = new Dictionary<string, IEnumerable<string>>();
            foreach (string file in storyFiles)
            {
                allStories.Add(file, GetStorySections(file));
            }
            //Parallel.ForEach(storyFiles, file => { allStories.TryAdd(file, GetStorySections(file)); });
            return allStories;
        }

        private IEnumerable<string> GetSourceWords()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(DelayMs);
                return File.ReadAllLines(@"Data\dictionary.txt");
            }).Result;
        }

        private IEnumerable<string> ListStoryFiles()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(DelayMs);
                return Directory.GetFiles(@"Data\Stories\", "*.txt");
            }).Result;
        }

        private IEnumerable<string> GetStorySections(string storyFile)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(DelayMs);
                string filePath = storyFile;
                var storySource = File.ReadAllText(filePath);

                return storySource.Split(new[] { '^' }, StringSplitOptions.None);
            }).Result;
        }

        private IEnumerable<TextSection> ProcessStory(IEnumerable<string> sourceWords, IEnumerable<string> sections)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(DelayMs);
                var finalGroups = new List<TextSection> { new TextSection(sections.First()) };

                foreach (string section in sections.Skip(1))
                {
                    finalGroups.Add(new TextSection(sourceWords.ElementAt(_rand.Next(sourceWords.Count() - 1))) { Format = true });
                    finalGroups.Add(new TextSection(section));
                }
                return finalGroups;
            }).Result;
        }
    }
}