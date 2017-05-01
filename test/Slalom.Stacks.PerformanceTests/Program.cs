using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
#pragma warning disable 1591

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
