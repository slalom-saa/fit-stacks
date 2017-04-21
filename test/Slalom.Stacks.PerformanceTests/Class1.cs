using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Running;
using Slalom.Stacks.Services.Inventory;
using Xunit;

namespace Slalom.Stacks.PerformanceTests
{
    public class StackStartupBenchmark
    {
        [Benchmark]
        public void Initialize()
        {
            using (var stack = new Stack())
            {
            }
        }
    }

    public class GenerateServiceRegistryBenchmark
    {
        [Benchmark]
        public void Initialize()
        {
            using (var stack = new Stack())
            {
                var service = stack.GetServices();
            }
        }
    }
}
