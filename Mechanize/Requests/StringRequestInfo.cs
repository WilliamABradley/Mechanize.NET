namespace Mechanize.Requests
{
    /// <summary>
    /// A String Request item, used for string Key/Value Pairs in Forms.
    /// </summary>
    public class StringRequestInfo : IRequestInfo
    {
        /// <summary>
        /// Constructor for <see cref="StringRequestInfo"/>.
        /// </summary>
        /// <param name="Value">The Value to Transmit</param>
        public StringRequestInfo(string Value)
        {
            this.Value = Value;
        }

        /// <summary>
        /// The Value to Transmit
        /// </summary>
        public readonly string Value;
    }
}