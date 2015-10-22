using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BadLibs.Test
{
    [TestClass]
    public class StoryEngineTests
    {
        [TestMethod]
        public async Task StoryEngine_CreateStory_ReturnsText()
        {
            var storyEngine = new StoryEngine();
            var textSections = await storyEngine.CreateStoryAsync(1, new System.Threading.CancellationToken());
            Assert.IsTrue(textSections.Count() > 10);
        }
    }
}
