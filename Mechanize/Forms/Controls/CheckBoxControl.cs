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

using Mechanize.Forms.Controls.Options;
using Mechanize.Html;
using Mechanize.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// CheckBox Control <para/>
    /// Covers: INPUT/CHECKBOX <para/>
    /// Allows multiple Options to be selected at any time.
    /// </summary>
    public class CheckBoxControl : ListControl
    {
        internal CheckBoxControl(HtmlForm Form, IHtmlNode Node) : base(Form, Node)
        {
            AddOption(Node);
        }

        internal void AddOption(IHtmlNode Option)
        {
            _Options.Add(new CheckBoxOption(this, Option));
        }

        /// <summary>
        /// De-Selects the Provided option, if it is an option for this form.
        /// </summary>
        /// <param name="Value">Value of the Option to De-Select.</param>
        public void DeSelect(string Value)
        {
            var option = FindOption(Value);
            if (option == null)
            {
                throw new Exception("This is not an option for this form");
            }
            DeSelect(option);
        }

        /// <summary>
        /// De-Selects the Provided option, if it is an option for this form.
        /// </summary>
        /// <param name="Option">Option to De-Select.</param>
        public void DeSelect(ListOption Option)
        {
            Option.Selected = false;
        }

        /// <summary>
        /// Gets the Selected Items to add to the Submission Form.
        /// </summary>
        /// <returns>Selected Items to add to the Submission Form.</returns>
        internal override List<IRequestInfo> GetRequestInfo()
        {
            return Selected
                .Select(item => (IRequestInfo)new StringRequestInfo(item.TransmitValue))
                .ToList();
        }

        /// <summary>
        /// The underlying value from the Selection, multiple Selected options can be specified, by separating the TransmitValue with a semi-colon (";").
        /// </summary>
        public override string Value
        {
            get
            {
                return string.Join(";", Selected.Select(item => item.TransmitValue));
            }
            set
            {
                var newoptions = value.Split(';');
                foreach (var option in _Options)
                {
                    option.Selected = false;
                }
                foreach (var option in newoptions)
                {
                    Select(option);
                }
            }
        }

        /// <summary>
        /// The Available options for this Control to Select from.
        /// </summary>
        public override IReadOnlyList<ListOption> Options => _Options;

        /// <summary>
        /// The Checkboxes that are currently selected for this Group. Use <see cref="ListControl.Select(ListOption)"/> to add a Selected Item, or use <see cref="DeSelect(ListOption)"/> to deselect a Selected item.
        /// </summary>
        public IReadOnlyList<CheckBoxOption> Selected => _Options.Cast<CheckBoxOption>()
            .Where(item => item.Selected)
            .ToList();
    }
}