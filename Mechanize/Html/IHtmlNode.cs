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

using System.Collections.Generic;

namespace Mechanize.Html
{
    /// <summary>
    /// An Html Node in the <see cref="IHtmlDocument"/> DOM Tree.
    /// </summary>
    public interface IHtmlNode
    {
        /// <summary>
        /// Gets the String value of an Attribute.
        /// </summary>
        /// <param name="Attribute">Attribute to Get.</param>
        /// <param name="Default">Default Value.</param>
        /// <returns>String value of Attribute</returns>
        string GetAttribute(string Attribute, string Default = null);

        /// <summary>
        /// Gets the Boolean value of an Attribute.
        /// </summary>
        /// <param name="Attribute">Attribute to Get.</param>
        /// <param name="Default">Default Value.</param>
        /// <returns>Boolean State of Attribute</returns>
        bool GetAttribute(string Attribute, bool Default = false);

        /// <summary>
        /// Sets an Attribute with a String Value.
        /// </summary>
        /// <param name="Attribute">Attribute to Set.</param>
        /// <param name="Value">Value to set for the Attribute.</param>
        void SetAttribute(string Attribute, string Value);

        /// <summary>
        /// Determines if a Class is used for this Node.
        /// </summary>
        /// <param name="ClassName">Name of the Class to Check.</param>
        /// <returns>Does the Node have the Class?</returns>
        bool HasClass(string ClassName);

        /// <summary>
        /// The Classes that belong to this Node.
        /// </summary>
        IEnumerable<string> Classes { get; }

        /// <summary>
        /// The Descendant Nodes for this Node.
        /// </summary>
        IEnumerable<IHtmlNode> Descendants { get; }

        /// <summary>
        /// The Attributes for this Node.
        /// </summary>
        IEnumerable<IHtmlAttribute> Attributes { get; }

        /// <summary>
        /// The name of the Tags for this Node.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Inner Text in between the Node's Tags.
        /// </summary>
        string InnerText { get; }
    }
}