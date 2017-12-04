using HtmlAgilityPack;
using System.Threading.Tasks;

namespace Mechanize.Forms.Controls
{
    public class SubmitControl : HtmlFormControl
    {
        internal SubmitControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
        }

        public Task<WebPage> Submit()
        {
            return Form.SubmitForm(this);
        }
    }
}