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

using Mechanize.Html;
using System.Linq;

namespace Mechanize.Forms.Controls.Options
{
    /// <summary>
    /// An option pertaining to a <see cref="CheckBoxControl"/>.
    /// </summary>
    public class CheckBoxOption : ListOption
    {
        internal CheckBoxOption(ListControl Parent, IHtmlNode Node) : base(Parent, Node)
        {
            if (ID != null)
            {
                LabelNode = Parent.Form.Node.Descendants
                    .FirstOrDefault(item => item.GetAttribute("for", null) == ID);
            }

            Selected = Node.Attributes
                .FirstOrDefault(item => item.Name == "checked") != null;
        }

        /// <summary>
        /// The Label text for this Option.
        /// </summary>
        public override string Label => LabelNode?.InnerText;

        /// <summary>
        /// The Value used for creating the Submission form, defaults to "on" if selected and no Value attribute exists.
        /// </summary>
        public override string TransmitValue => !string.IsNullOrWhiteSpace(Value) ? Value : Selected ? "on" : null;

        /// <summary>
        /// The Underlying node for this option's Label.
        /// </summary>
        public readonly IHtmlNode LabelNode;
    }
}