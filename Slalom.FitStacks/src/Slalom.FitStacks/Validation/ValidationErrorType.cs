namespace Slalom.FitStacks.Validation
{
    /// <summary>
    /// Indicates the validation error type.
    /// </summary>
    public enum ValidationErrorType
    {
        /// <summary>
        /// Indicates a type was not specified.
        /// </summary>
        None,

        /// <summary>
        /// Indicates an input validation message.
        /// </summary>
        Input,

        /// <summary>
        /// Indicates an security validation message.
        /// </summary>
        Security,

        /// <summary>
        /// Indicates a business validation message.
        /// </summary>
        Business
    }
}