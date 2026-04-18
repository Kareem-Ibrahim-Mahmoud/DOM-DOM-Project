using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QudraSaaS.Dmain.IRepositories;
using Serilog;

namespace QudraSaaS.Infrastructure.loges
{
    public class SerilogLoggerService: ILoggerService
    {
        public void LogInformation(string message)
        => Log.Information(message);

        public void LogWarning(string message)
            => Log.Warning(message);

        public void LogError(string message, Exception ex = null)
            => Log.Error(ex, message);
    }
}
