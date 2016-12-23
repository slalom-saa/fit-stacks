using System;

namespace Slalom.Stacks.Utilities.NewId.NewIdProviders
{
    public class DateTimeTickProvider :
        ITickProvider
    {
        public long Ticks
        {
            get { return DateTime.UtcNow.Ticks; }
        }
    }
}