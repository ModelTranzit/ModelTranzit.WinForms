using Dizignit.Core.Enums;

namespace Dizignit.Core.Interfaces
{
    public interface ILogBytes
    {
        void Log(byte[] message, ETileType imageType);
    }
}
