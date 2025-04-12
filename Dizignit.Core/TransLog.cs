using Dizignit.Core.Interfaces;

namespace Dizignit.Core
{
    public class Trans
    {
        private readonly ILogger _logger;

        public DateTime UTCTransStartUTC { get; set; }
        public byte[] BinaryData { get; set; }

        public Trans()
        {
            UTCTransStartUTC = DateTime.UtcNow;
        }

        public bool Log()
        {
            return _logger.Log();
        }
    }
}
