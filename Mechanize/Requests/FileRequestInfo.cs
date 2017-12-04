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