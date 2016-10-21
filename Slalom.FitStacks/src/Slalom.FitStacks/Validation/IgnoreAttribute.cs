using System;

namespace Slalom.FitStacks.Validation
{
    /// <summary>
    /// Indicates that a property should be ignored when logged.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IgnoreAttribute : Attribute
    {
    }
}