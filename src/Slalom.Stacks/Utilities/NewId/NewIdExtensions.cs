using System;

namespace Slalom.Stacks.Utilities.NewId
{
    public static class NewIdExtensions
    {
        public static NewId ToNewId(this Guid guid)
        {
            return new NewId(guid.ToByteArray());
        }
    }
}