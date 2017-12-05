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

using HtmlAgilityPack;
using Mechanize.Html;

namespace Mechanize.HtmlAgility
{
    /// <summary>
    /// The Container for <see cref="HtmlDocument"/>.
    /// </summary>
    public class HtmlAgilityDocument : IHtmlDocument
    {
        internal HtmlAgilityDocument(HtmlDocument Document)
        {
            this.Document = Document;
            DocumentNode = new HtmlAgilityNode(Document.DocumentNode);
        }

        /// <summary>
        /// The Document's top Node.
        /// </summary>
        public IHtmlNode DocumentNode { get; }

        /// <summary>
        /// The Underlying Document Instance.
        /// </summary>
        public readonly HtmlDocument Document;
    }
}