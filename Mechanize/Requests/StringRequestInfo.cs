// ******************************************************************
// Copyright (c) William Bradley
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

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