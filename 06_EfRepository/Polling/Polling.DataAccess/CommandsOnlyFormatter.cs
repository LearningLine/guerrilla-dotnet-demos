using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polling.DataAccess.Test
{
    public class CommandsOnlyFormatter : DatabaseLogFormatter
    {
        public CommandsOnlyFormatter(DbContext context, Action<string> writeAction)
            : base(context, writeAction)
        {
        }

        public override void Opened(
            DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
        }

        public override void LogCommand<TResult>(
            DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            Write(command.CommandText);
        }
        public override void Closed(
            DbConnection connection, DbConnectionInterceptionContext interceptionContext)
        {
        }

        public override void LogResult<TResult>(
            DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
        }
    }
}
