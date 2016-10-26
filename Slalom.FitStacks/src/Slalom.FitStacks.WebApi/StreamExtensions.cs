using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slalom.FitStacks.WebApi
{
    public static class StreamExtensions
    {
        public static string ReadAsString(this Stream instance)
        {
            using (var reader = new StreamReader(instance, Encoding.UTF8, false, 512, true))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
