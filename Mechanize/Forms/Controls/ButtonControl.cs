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
using Mechanize.Requests;
using System.Collections.Generic;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// Control that we are not interested in.<para/>
    /// Covers Html elements: INPUT/RESET, BUTTON/RESET, INPUT/BUTTON, BUTTON/BUTTON. <para/>
    /// These controls are always unsuccessful, in the terminology of HTML 4 (ie. they never require any information to be returned to the server). <para/>
    /// BUTTON/BUTTON is used to generate events for script embedded in HTML. <para/>
    /// </summary>
    public class ButtonControl : HtmlFormControl
    {
        internal ButtonControl(HtmlForm Form, IHtmlNode Node) : base(Form, Node)
        {
        }

        /// <summary>
        /// Returns null, as this Control does not add to the Form.
        /// </summary>
        /// <returns>null</returns>
        internal override List<IRequestInfo> GetRequestInfo()
        {
            return null;
        }
    }
}