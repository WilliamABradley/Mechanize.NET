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
using Mechanize.Forms.Controls.Options;
using System.Collections.Generic;
using System.Linq;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// Covers: SELECT(and OPTION) <para/>
    /// SELECT control values and labels are subject to some messy defaulting rules.
    /// A Selection item will be posted by it's value attribute if it exists,
    /// if it doesn't exist, it defaults to label attribute if that exists,
    /// otherwise it defaults to the <see cref="ListOption.Name"/>, a.k.a. the Inner Text of the node. <para/>
    /// The Labels are sometimes more meaningful that the value of an option, and it is recommended to use the <see cref="ListOption"/> instance of the value, instead of setting with a string.
    /// </summary>
    public class SelectControl : ListControl
    {
        internal SelectControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
            Options.First().Selected = true;
        }

        /// <summary>
        /// The current Selected Item for this Control. Use <see cref="ListControl.Select(ListOption)"/> to change it.
        /// </summary>
        public ListOption Selected => Options.FirstOrDefault(item => item.Selected);

        /// <summary>
        /// The underlying value from the Selection, equivalent to using <see cref="ListControl.Select(string)"/>.
        /// </summary>
        public override string Value
        {
            get => Selected.TransmitValue;
            set
            {
                CheckSetAttribute();
                Select(value);
            }
        }

        /// <summary>
        /// The Available options for this Control to Select from.
        /// </summary>
        public override IReadOnlyList<ListOption> Options
        {
            get
            {
                if (_Options == null)
                {
                    var options = Node.Descendants().Where(item => item.Name.ToLower() == "option");
                    _Options = options.Select(item => (ListOption)new SelectOption(this, item)).ToList();
                }

                return _Options;
            }
        }
    }
}