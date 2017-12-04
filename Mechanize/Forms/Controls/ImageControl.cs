using HtmlAgilityPack;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// An Image control that acts like a <see cref="SubmitControl"/>. <para/>
    /// Covers: INPUT/IMAGE <para/>
    /// Can handle Coordinates when Submitting content. (Not Implemented yet).
    /// </summary>
    public class ImageControl : SubmitControl
    {
        internal ImageControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
        }
    }
}