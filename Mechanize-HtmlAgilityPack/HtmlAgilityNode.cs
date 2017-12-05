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
using System.Collections.Generic;
using System.Linq;

namespace Mechanize.HtmlAgility
{
    /// <summary>
    /// The Container for <see cref="HtmlNode"/>.
    /// </summary>
    public class HtmlAgilityNode : IHtmlNode
    {
        internal HtmlAgilityNode(HtmlNode Node)
        {
            this.Node = Node;
            Attributes = Node.Attributes.Select(item => new HtmlAgilityAttribute(item));
        }

        /// <summary>
        /// Gets the String value of an Attribute.
        /// </summary>
        /// <param name="Attribute">Attribute to Get.</param>
        /// <param name="Default">Default Value.</param>
        /// <returns>String value of Attribute</returns>
        public string GetAttribute(string Attribute, string Default = null)
        {
            return Node.GetAttributeValue(Attribute, Default);
        }

        /// <summary>
        /// Gets the Boolean value of an Attribute.
        /// </summary>
        /// <param name="Attribute">Attribute to Get.</param>
        /// <param name="Default">Default Value.</param>
        /// <returns>Boolean State of Attribute</returns>
        public bool GetAttribute(string Attribute, bool Default = false)
        {
            return Node.GetAttributeValue(Attribute, Default);
        }

        /// <summary>
        /// Sets an Attribute with a String Value.
        /// </summary>
        /// <param name="Attribute">Attribute to Set.</param>
        /// <param name="Value">Value to set for the Attribute.</param>
        public void SetAttribute(string Attribute, string Value)
        {
            Node.SetAttributeValue(Attribute, Value);
        }

        /// <summary>
        /// Determines if a Class is used for this Node.
        /// </summary>
        /// <param name="ClassName">Name of the Class to Check.</param>
        /// <returns>Does the Node have the Class?</returns>
        public bool HasClass(string ClassName)
        {
            return Node.HasClass(ClassName);
        }

        /// <summary>
        /// The Classes that belong to this Node.
        /// </summary>
        public IEnumerable<string> Classes => Node.GetClasses();

        /// <summary>
        /// The Descendant Nodes for this Node.
        /// </summary>
        public IEnumerable<IHtmlNode> Descendants => Node.Descendants().Select(item => new HtmlAgilityNode(item));

        /// <summary>
        /// The Attributes for this Node.
        /// </summary>
        public IEnumerable<IHtmlAttribute> Attributes { get; }

        /// <summary>
        /// The name of the Tags for this Node.
        /// </summary>
        public string Name => Node.Name;

        /// <summary>
        /// Gets the Inner Text in between the Node's Tags.
        /// </summary>
        public string InnerText => Node.InnerText;

        /// <summary>
        /// The Underlying Node Instance.
        /// </summary>
        public readonly HtmlNode Node;
    }
}