using System;

namespace Slalom.Stacks.Utilities.NewId.NewIdProviders
{
    internal class DateTimeTickProvider :
        ITickProvider
    {
        public long Ticks
        {
            get { return DateTime.UtcNow.Ticks; }
        }
    }
}