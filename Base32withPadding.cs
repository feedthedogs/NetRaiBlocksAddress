using System;
using System.Linq;
using System.Text;

namespace NetRaiBlocksAddress
{
    /// <summary>
    /// Allows some custom padding to the start of the array so that the address it eventally converts to is either a 1 or a 3, so don't use for normal base32 stuff
    /// </summary>
    static class Base32withPadding
    {
        public static string Encode(byte[] data, string alphabet, int desiredSize)
        {
            var binString = string.Concat(data.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
            binString = binString.PadLeft(desiredSize,'0');

            var final = new StringBuilder(desiredSize / 5);
            int num;
            for (int i = 0; i < desiredSize / 5; i++)
            {
                num = Convert.ToInt16(binString.Substring(i * 5, 5), 2);
                final.Append(alphabet[num]);
            }
            return final.ToString();
        }

        public static byte[] Decode(string data, string alphabet, int ignoreStartCount)
        {
            var binString = string.Concat(data.Select(b => Convert.ToString(alphabet.IndexOf(b), 2).PadLeft(5, '0')));
            binString = binString.Substring(ignoreStartCount);
            var final = new byte[binString.Length / 8];
            for (int i = 0; i < final.Length; i++)
            {
                final[i] = Convert.ToByte(binString.Substring(i * 8, 8), 2);
            }
            return final;
        }
    }
}
