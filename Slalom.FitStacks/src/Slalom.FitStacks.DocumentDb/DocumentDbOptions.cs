using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.FitStacks.DocumentDb
{
    public class DocumentDbOptions
    {
        public string Host { get; set; }

        public string Database { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Port { get; set; } = 10250;
    }
}
