using HtmlAgilityPack;
using Mechanize.Forms.Controls.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// Represents the Structure of a List based control, such as the <see cref="SelectControl"/>, <see cref="CheckBoxControl"/> or <see cref="RadioControl"/>.
    /// </summary>
    public abstract class ListControl : ScalarControl
    {
        internal ListControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
        }

        /// <summary>
        /// Selects the Provided option, if it is an option for this form.
        /// </summary>
        /// <param name="Value">Value of the Option to Select.</param>
        public void Select(string Value)
        {
            var option = FindOption(Value);
            if (option == null)
            {
                throw new Exception("This is not an option for this form");
            }
            Select(option);
        }

        /// <summary>
        /// Selects the Provided option, if it is an option for this form.
        /// </summary>
        /// <param name="Option">Option to Select.</param>
        public void Select(ListOption Option)
        {
            if (Option.Parent != this)
            {
                throw new Exception("This is not an option for this form");
            }
            Option.Selected = true;
        }

        /// <summary>
        /// Finds an Option from the specified Value, checking against Value, Label and Name Properties.
        /// </summary>
        /// <param name="Value">Value to Find option for.</param>
        /// <returns>The Specifed Option if found; Otherwise null.</returns>
        public ListOption FindOption(string Value)
        {
            return Options.FirstOrDefault(item =>
                Value.Equals(item.Value, StringComparison.InvariantCultureIgnoreCase)
                || Value.Equals(item.Label, StringComparison.InvariantCultureIgnoreCase)
                || Value.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// The Available options for this Control to Select from.
        /// </summary>
        public abstract IReadOnlyList<ListOption> Options { get; }

        protected List<ListOption> _Options;
    }
}