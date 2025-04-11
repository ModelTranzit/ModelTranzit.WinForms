using Dizignit.Core.Enums;

namespace Dizignit.Core.Helpers
{
    public static class TileHelper
    {
        public static EColorDepth ColorDepth(int colorDepth)
        {
            return colorDepth switch
            {
                1 => EColorDepth.Bit1,
                4 => EColorDepth.Bit4,
                8 => EColorDepth.Bit8,
                16 => EColorDepth.Bit16,
                24 => EColorDepth.Bit24,
                _ => EColorDepth.UnsupportedBitDepth
            };
        }
    }
}
