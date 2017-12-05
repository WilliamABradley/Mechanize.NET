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

using AngleSharp.Dom;
using Mechanize.Html;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mechanize.ParseAngleSharp
{
    /// <summary>
    /// The Container for <see cref="IElement"/>.
    /// </summary>
    public class AngleSharpNode : IHtmlNode
    {
        internal AngleSharpNode(IElement Element)
        {
            this.Element = Element;
        }

        /// <summary>
        /// Gets the String value of an Attribute.
        /// </summary>
        /// <param name="Attribute">Attribute to Get.</param>
        /// <param name="Default">Default Value.</param>
        /// <returns>String value of Attribute</returns>
        public string GetAttribute(string Attribute, string Default = null)
        {
            var attribute = Element.GetAttribute(Attribute);
            return !string.IsNullOrWhiteSpace(attribute) ? attribute : Default;
        }

        /// <summary>
        /// Gets the Boolean value of an Attribute.
        /// </summary>
        /// <param name="Attribute">Attribute to Get.</param>
        /// <param name="Default">Default Value.</param>
        /// <returns>Boolean State of Attribute</returns>
        public bool GetAttribute(string Attribute, bool Default = false)
        {
            var attribute = GetAttribute(Attribute, null);
            return !string.IsNullOrWhiteSpace(attribute) ? Convert.ToBoolean(attribute) : Default;
        }

        /// <summary>
        /// Sets an Attribute with a String Value.
        /// </summary>
        /// <param name="Attribute">Attribute to Set.</param>
        /// <param name="Value">Value to set for the Attribute.</param>
        public void SetAttribute(string Attribute, string Value)
        {
            Element.SetAttribute(Attribute, Value);
        }

        /// <summary>
        /// Determines if a Class is used for this Node.
        /// </summary>
        /// <param name="ClassName">Name of the Class to Check.</param>
        /// <returns>Does the Node have the Class?</returns>
        public bool HasClass(string ClassName)
        {
            return Element.ClassList.Contains(ClassName);
        }

        /// <summary>
        /// The Classes that belong to this Node.
        /// </summary>
        public IEnumerable<string> Classes => Element.ClassList;

        /// <summary>
        /// The Descendant Nodes for this Node.
        /// </summary>
        public IEnumerable<IHtmlNode> Descendants => Element.ChildNodes.OfType<IElement>().Select(item => new AngleSharpNode(item));

        /// <summary>
        /// The Attributes for this Node.
        /// </summary>
        public IEnumerable<IHtmlAttribute> Attributes => Element.Attributes.Select(item => new AngleSharpAttribute(item));

        /// <summary>
        /// The name of the Tags for this Node.
        /// </summary>
        public string Name => Element.LocalName;

        /// <summary>
        /// Gets the Inner Text in between the Node's Tags.
        /// </summary>
        public string InnerText => Element.TextContent;

        /// <summary>
        /// The Underlying Element Instance.
        /// </summary>
        public readonly IElement Element;
    }
}