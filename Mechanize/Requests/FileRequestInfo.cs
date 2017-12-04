namespace Mechanize.Requests
{
    /// <summary>
    /// A File Request item, used for transmitting Files using MultiPart/FormData.
    /// </summary>
    public class FileRequestInfo : IRequestInfo
    {
        /// <summary>
        /// Constructor for <see cref="FileRequestInfo"/>.
        /// </summary>
        /// <param name="FileData">File Data to transmit.</param>
        /// <param name="FileName">Optional File Name to transmit.</param>
        public FileRequestInfo(byte[] FileData, string FileName = null)
        {
            this.FileData = FileData;
            this.FileName = FileName;
        }

        /// <summary>
        /// Optional File Name to transmit.
        /// </summary>
        public readonly string FileName;

        /// <summary>
        /// File Data to transmit.
        /// </summary>
        public readonly byte[] FileData;
    }
}