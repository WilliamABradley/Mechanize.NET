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
using System.Threading.Tasks;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// Covers: INPUT/SUBMIT, BUTTON/SUBMIT <para/>
    /// Used to submit the Form. If a form contains more than one Submit Button, the control's name will be included in the Form Data. <para/>
    /// In this implementation, it uses the <see cref="HtmlFormControl.Name"/> as the Key, and <see cref="HtmlFormControl.Value"/> as the Value. <para/>
    /// A.K.A. a Submission control with Name: btnI and Inner Text: I'm feeling lucky, will submit ?btnI=I%27m%20feeling%20lucky. Such as with Google Search.
    /// </summary>
    public class SubmitControl : HtmlFormControl
    {
        internal SubmitControl(HtmlForm Form, IHtmlNode Node) : base(Form, Node)
        {
        }

        /// <summary>
        /// Submits the form, using this as the provided button.
        /// </summary>
        /// <returns>The WebPage Result.</returns>
        public Task<WebPage> SubmitAsync()
        {
            return Form.SubmitForm(this);
        }

        /// <summary>
        /// Returns the Value as the Request.
        /// </summary>
        /// <returns>Request Info.</returns>
        internal override List<IRequestInfo> GetRequestInfo()
        {
            return new List<IRequestInfo> { new StringRequestInfo(GetAttribute("value")) };
        }
    }
}