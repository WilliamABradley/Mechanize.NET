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