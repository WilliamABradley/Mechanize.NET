namespace Mechanize.Requests
{
    public class StringRequestInfo : IRequestInfo
    {
        public StringRequestInfo(string Value)
        {
            this.Value = Value;
        }

        public readonly string Value;
    }
}