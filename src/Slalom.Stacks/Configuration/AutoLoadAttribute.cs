using System;
using System.Linq;

namespace Slalom.Stacks.Configuration
{
    /// <summary>
    /// Indicates that the class should be loaded with any loading routines.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoLoadAttribute : Attribute
    {
    }
}