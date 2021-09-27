using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StartUpWebAPI.Models
{
    /// <summary>
    /// Class for converting streams to bytes.
    /// </summary>
    public class StreamToBytesConverter
    {
        /// <summary>
        /// Converts the given stream to byte array representation.
        /// </summary>
        /// <param name="source">The source of stream.</param>
        /// <returns></returns>
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