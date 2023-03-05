using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services.LogService
{
    public class LogService : ILogService
    {
        public void WriteConsole(string logMessage)
        {
            Console.WriteLine(logMessage);
        }
    }
}
