using System;

namespace Slalom.FitStacks.Validation
{
    /// <summary>
    /// Indicates that a property should be handled securely when logged.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SecureAttribute : Attribute
    {
    }
}