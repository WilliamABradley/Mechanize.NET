using HtmlAgilityPack;
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
        internal ButtonControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
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