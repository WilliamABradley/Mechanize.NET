using HtmlAgilityPack;
using System;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// Control that we are not interested in.<para/>
    /// Covers Html elements: INPUT/RESET, BUTTON/RESET, INPUT/BUTTON, BUTTON/BUTTON. <para/>
    /// These controls are always unsuccessful, in the terminology of HTML 4 (ie. they never require any information to be returned to the server). <para/>
    /// BUTTON/BUTTON is used to generate events for script embedded in HTML. <para/>
    /// The value attribute of IgnoreControl is always null.
    /// </summary>
    public class IgnoreControl : HtmlFormControl
    {
        internal IgnoreControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
        }

        /// <summary>
        /// Don't touch this.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        public override string Value { get => null; set => throw new NotSupportedException("Ignore this Control"); }
    }
}