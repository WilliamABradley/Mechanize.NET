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
using Mechanize.Forms.Controls;
using Mechanize.Requests;
using System;
using System.Collections.Generic;

namespace Mechanize.Forms
{
    /// <summary>
    /// The Base Form Control Class, Used to Get Name and Set Value, as an abstraction of all Form Controls.
    /// </summary>
    public abstract class HtmlFormControl
    {
        internal HtmlFormControl(HtmlForm Form, HtmlNode Node)
        {
            this.Form = Form;
            this.Node = Node;
            Type = GetControlType(Node);
        }

        /// <summary>
        /// The Html "id" Attribute for this Control.
        /// </summary>
        public string ID => Node.Id;

        /// <summary>
        /// The Html "disabled" attribute. If <see cref="Disabled"/>, an exception will be thrown when attempting to change value.
        /// </summary>
        public bool Disabled => Node.GetAttributeValue("disabled", false);

        /// <summary>
        /// The Html "readonly" attribute. If <see cref="ReadOnly"/>, an exception will be thrown when attempting to change value.
        /// </summary>
        public bool ReadOnly => Node.GetAttributeValue("readonly", false);

        /// <summary>
        /// The Html "name" Attribute for this control.
        /// </summary>
        public string Name
        {
            get => GetAttribute("name");
        }

        /// <summary>
        /// Gets an Html Attribute from the Underlying Node.
        /// </summary>
        /// <param name="Attribute">Attribute to get.</param>
        /// <returns>Attribute Value</returns>
        public string GetAttribute(string Attribute)
        {
            return Node.GetAttributeValue(Attribute, null);
        }

        /// <summary>
        /// Gets an Html Attribute from the Underlying Node.
        /// </summary>
        /// <param name="Attribute">Attribute to get.</param>
        /// <param name="Default">Default value if Attribute doesn't exist.</param>
        /// <returns>Attribute Value</returns>
        public string GetAttribute(string Attribute, string Default)
        {
            return GetAttribute(Attribute) ?? Default;
        }

        /// <summary>
        /// Sets an Html Attribute to the Underlying Node.
        /// </summary>
        /// <param name="Attribute">Attribute to set.</param>
        /// <param name="Value"></param>
        public virtual void SetAttribute(string Attribute, string Value)
        {
            Node.SetAttributeValue(Attribute, Value);
        }

        /// <summary>
        /// Gets a List of Attributes to add to the Form Submission.
        /// </summary>
        /// <returns>List of Attributes to add to the Form Submission</returns>
        internal abstract List<IRequestInfo> GetRequestInfo();

        /// <summary>
        /// The Type of Form Control that this Control pertains to.
        /// </summary>
        public readonly FormControlType Type;

        /// <summary>
        /// The Underlying Html Node that this Control Pertains to.
        /// </summary>
        public readonly HtmlNode Node;

        /// <summary>
        /// The Html Form that this Control belongs to.
        /// </summary>
        internal readonly HtmlForm Form;

        /// <summary>
        /// Gets the associated <see cref="FormControlType"/> for the provided HtmlNode.
        /// </summary>
        /// <param name="Node">Node to Evaluate.</param>
        /// <returns>The Type of the Form Control</returns>
        internal static FormControlType GetControlType(HtmlNode Node)
        {
            // Check agains the Control Name.
            var type = GetControlType(Node.Name.ToLower());
            if (type == FormControlType.Unknown)
            {
                // Fetch the type attribute.
                var typeattr = Node.GetAttributeValue("type", "text");
                // Check against type attribute.
                type = GetControlType(typeattr);
                if (type == FormControlType.Unknown)
                {
                    // Pretty much the same as before, but looser.
                    type = (FormControlType)Enum.Parse(typeof(FormControlType), typeattr, true);
                }
            }
            return type;
        }

        /// <summary>
        /// Gets the associated <see cref="FormControlType"/> for the provided String.
        /// </summary>
        /// <param name="Type">String to Evaluate.</param>
        /// <returns>The Type of the Form Control</returns>
        internal static FormControlType GetControlType(string Type)
        {
            switch (Type)
            {
                case "select":
                    return FormControlType.Select;

                case "button":
                case "buttonbutton":
                case "reset":
                case "resetbutton":
                    return FormControlType.Ignore;

                case "radio":
                    return FormControlType.Radio;

                case "checkbox":
                    return FormControlType.Checkbox;

                case "image":
                    return FormControlType.Image;

                case "file":
                    return FormControlType.File;

                case "password":
                    return FormControlType.Password;

                case "textarea":
                    return FormControlType.TextArea;

                case "text":
                    return FormControlType.Text;

                case "submit":
                case "submitbutton":
                    return FormControlType.Submit;

                case "hidden":
                    return FormControlType.Hidden;

                default:
                    return FormControlType.Unknown;
            }
        }
    }
}