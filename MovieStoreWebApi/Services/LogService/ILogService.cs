using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services.LogService
{
    public interface ILogService
    {
        public void WriteConsole(string logMessage);
    }
}
