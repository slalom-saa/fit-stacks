using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace Slalom.Stacks.PerformanceTests
{
    public class Program
    {
        public static void Main()
        {
            var result = BenchmarkRunner.Run<GenerateServiceRegistryBenchmark>();
        }
    }
}
