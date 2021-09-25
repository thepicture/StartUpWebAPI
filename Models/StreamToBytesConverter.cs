using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Models
{
    public class StreamToBytesConverter
    {
        public static byte[] Convert(Stream source)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                source.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}