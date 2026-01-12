using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soap2Day.Util.Logger
{
    public static class AppLogger {
        public static void LogError(string error) {
            File.AppendAllText("Logs/error.log", $"[{DateTime.Now}] {error}{Environment.NewLine}");
        }
    }
}