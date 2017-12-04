using System;

namespace Mechanize.Exceptions
{
    /// <summary>
    /// Thrown when an invalid operation occurs during operation of the <see cref="MechanizeBrowser"/>.
    /// </summary>
    public class MechanizeBrowserStateException : Exception
    {
        internal MechanizeBrowserStateException(string Message) : base(Message)
        {
        }
    }
}