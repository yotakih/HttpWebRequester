using System;
using System.Text;

namespace CommonMDLProj
{
    public static class CommonMdl
    {
        public static Encoding GetEncoding(string enc)
        {
            switch(enc.ToLower())
            {
                case "ascii":
                    return Encoding.ASCII;
                case "unicode":
                    return Encoding.Unicode;
                case "utf8":
                case "utf-8":
                    return Encoding.UTF8;
                default:
                    return Encoding.GetEncoding(enc);
            }
        }
    }
}
