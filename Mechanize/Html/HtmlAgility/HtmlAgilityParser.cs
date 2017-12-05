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

namespace Mechanize.Html.HtmlAgility
{
    /// <summary>
    /// Creates a Parser for constructing the <see cref="HtmlAgilityDocument"/> from a string. <para/>
    /// <see cref="HtmlAgilityDocument"/> is a Container for <see cref="HtmlDocument"/>.
    /// </summary>
    public class HtmlAgilityParser : IHtmlParser
    {
        /// <summary>
        /// Parses an Html string into a <see cref="HtmlAgilityDocument"/>.
        /// </summary>
        /// <param name="Html">Html to Parse</param>
        /// <returns>The Html Document.</returns>
        public IHtmlDocument Parse(string Html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(Html);
            return new HtmlAgilityDocument(document);
        }
    }
}