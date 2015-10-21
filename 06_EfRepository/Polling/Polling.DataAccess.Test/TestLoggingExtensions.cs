using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Polling.DataAccess.Test
{
    public static class TestLoggingExtensions
    {
        public static IEnumerable<string> FilterOutMigrationLogs(this IEnumerable<string> logs)
        {
            return logs.Where(l => !l.Contains("__MigrationHistory") && !l.Contains("EdmMetadata"));
        }

        public static MemoryTarget SetupNLogMemoryTarget()
        {
            var config = new LoggingConfiguration();
            var memoryTarget = new MemoryTarget { Layout = "${message}" };
            config.AddTarget("memory", memoryTarget);
            var ruleDebug = new LoggingRule("*", LogLevel.Debug, memoryTarget);
            config.LoggingRules.Add(ruleDebug);
            LogManager.Configuration = config;
            return memoryTarget;
        }
    }
}
