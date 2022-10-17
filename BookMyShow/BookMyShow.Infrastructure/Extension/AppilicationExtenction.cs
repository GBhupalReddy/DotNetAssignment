using System.Text;

namespace BookMyShow.Infrastructure.Extension
{
    public static class AppilicationExtenction
    {
        public static byte[] GetByteArray(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
        public static byte[] HexToByte(this string str)
        {
            var returnBytes = new byte[str.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
