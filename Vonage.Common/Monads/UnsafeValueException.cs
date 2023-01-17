namespace Vonage.Common.Monads
{
    /// <summary>
    ///     Represents errors that occur during unsafe value retrieval.
    /// </summary>
    public class UnsafeValueException : Exception
    {
        /// <summary>
        ///     Creates an UnsafeValueException.
        /// </summary>
        /// <param name="message">The message.</param>
        public UnsafeValueException(string message) : base(message)
        {
        }
    }
}