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

using AngleSharp.Parser.Html;
using Mechanize.Html;

namespace Mechanize.ParseAngleSharp
{
    /// <summary>
    /// Creates a Parser for constructing the <see cref="AngleSharpDocument"/> from a string. <para/>
    /// <see cref="AngleSharpDocument"/> is a Container for <see cref="AngleSharp.Dom.Html.IHtmlDocument"/>.
    /// </summary>
    public class AngleSharpParser : IHtmlParser
    {
        /// <summary>
        /// Constructor for the <see cref="AngleSharpParser"/>.
        /// </summary>
        public AngleSharpParser()
        {
            Parser = new HtmlParser();
        }

        /// <summary>
        /// Parses an Html string into a <see cref="AngleSharpDocument"/>.
        /// </summary>
        /// <param name="Html">Html to Parse</param>
        /// <returns>The Html Document.</returns>
        public IHtmlDocument Parse(string Html)
        {
            return new AngleSharpDocument(Parser.Parse(Html));
        }

        /// <summary>
        /// The Underlying Parser.
        /// </summary>
        public readonly HtmlParser Parser;
    }
}