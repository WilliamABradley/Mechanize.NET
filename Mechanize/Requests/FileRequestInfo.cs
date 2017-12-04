namespace Mechanize.Requests
{
    public class FileRequestInfo : IRequestInfo
    {
        public FileRequestInfo(byte[] FileData, string FileName = null)
        {
            this.FileData = FileData;
            this.FileName = FileName;
        }

        public readonly string FileName;

        public readonly byte[] FileData;
    }
}