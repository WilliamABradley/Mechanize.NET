using System;

namespace Mechanize.Exceptions
{
    public class BrowserStateException : Exception
    {
        internal BrowserStateException(string Message) : base(Message)
        {
        }
    }
}