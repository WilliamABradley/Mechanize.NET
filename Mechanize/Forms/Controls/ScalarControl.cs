using HtmlAgilityPack;
using Mechanize.Requests;
using System;
using System.Collections.Generic;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// Represents a <see cref="HtmlFormControl"/> that supports setting the "value" attribute in Html.
    /// </summary>
    public abstract class ScalarControl : HtmlFormControl
    {
        internal ScalarControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
        }

        /// <summary>
        /// Checks whether the Control is locked, and if unable to be modified, and throws an Exception if Locked.
        /// </summary>
        protected void CheckSetAttribute()
        {
            if (Disabled) throw new Exception($"Control {Value ?? Node.Name} is disabled");
            else if (ReadOnly) throw new Exception($"Control {Value ?? Node.Name} is readonly");
        }

        /// <summary>
        /// Returns Request Info with the Value as the result.
        /// </summary>
        /// <returns>Request Info.</returns>
        internal override List<IRequestInfo> GetRequestInfo()
        {
            return new List<IRequestInfo> { new StringRequestInfo(Value) };
        }

        /// <summary>
        /// The html "value" attribute for this Control, this is used for Populating and Submitting forms.
        /// </summary>
        public virtual string Value
        {
            get => GetAttribute("value");
            set => SetAttribute("value", value);
        }
    }
}