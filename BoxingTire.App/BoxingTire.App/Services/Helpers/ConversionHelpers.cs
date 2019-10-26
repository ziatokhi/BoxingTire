using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingTire.App.Services.Helpers
{
    public static class ConversionHelpers
    {
        public static short ByteArrayToShort16BitLittleEndian(byte[] val)
        {
            if (val.Length != 2)
                throw new ArgumentException("Argument is not 2 byte long. ", nameof(val));

            return (short)((val[1] << 8) + val[0]);
        }
    }
}
