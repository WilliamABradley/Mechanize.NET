using HtmlAgilityPack;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// Textual Input Control. <para/>
    /// Covers HTML elements: INPUT/TEXT, INPUT/PASSWORD, INPUT/HIDDEN, TEXTAREA
    /// </summary>
    public class TextInputControl : ScalarControl
    {
        internal TextInputControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
        }
    }
}