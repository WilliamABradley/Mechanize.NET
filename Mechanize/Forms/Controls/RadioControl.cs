using HtmlAgilityPack;
using Mechanize.Forms.Controls.Options;
using System.Collections.Generic;
using System.Linq;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// Radio Control <para/>
    /// Covers: INPUT/RADIO <para/>
    /// Only allows one Option to be selected at any time.
    /// </summary>
    public class RadioControl : ListControl
    {
        internal RadioControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
            AddOption(Node);
        }

        /// <summary>
        /// Adds an option to the Group.
        /// </summary>
        /// <param name="Option">Node to Parse and add.</param>
        internal void AddOption(HtmlNode Option)
        {
            _Options.Add(new RadioOption(this, Option));
        }

        /// <summary>
        /// The Available options for this Control to Select from.
        /// </summary>
        public override IReadOnlyList<ListOption> Options => _Options;

        /// <summary>
        /// The current Selected Item for this Control. Use <see cref="ListControl.Select(ListOption)"/> to change it.
        /// </summary>
        public RadioOption Selected => _Options.Cast<RadioOption>()
            .FirstOrDefault(item => item.Selected);

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
    }
}