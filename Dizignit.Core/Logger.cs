using Dizignit.Core.Interfaces;

namespace Dizignit.Core
{
    public class Logger : ILogger
    {
        private Trans _log;

        public Logger()
        {
               // need to add dependency injection later 
               _log = new Trans();
        }

        public bool Log()
        {
            return _log.Log();
        }
    }
}
