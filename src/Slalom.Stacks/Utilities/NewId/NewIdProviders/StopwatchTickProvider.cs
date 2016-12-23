using System;
using System.Diagnostics;

namespace Slalom.Stacks.Utilities.NewId.NewIdProviders
{
    public class StopwatchTickProvider :
        ITickProvider
    {
        readonly Stopwatch _stopwatch;
        DateTime _start;

        public StopwatchTickProvider()
        {
            _start = DateTime.UtcNow;
            _stopwatch = Stopwatch.StartNew();
        }

        public long Ticks
        {
            get { return (_start.AddTicks(_stopwatch.Elapsed.Ticks)).Ticks; }
        }
    }
}