using HtmlAgilityPack;
using System.Linq;

namespace Mechanize.Forms.Controls.Options
{
    /// <summary>
    /// An option pertaining to a <see cref="CheckBoxControl"/>.
    /// </summary>
    public class CheckBoxOption : ListOption
    {
        internal CheckBoxOption(ListControl Parent, HtmlNode Node) : base(Parent, Node)
        {
            if (ID != null)
            {
                LabelNode = Parent.Form.Node.Descendants()
                    .FirstOrDefault(item => item.GetAttributeValue("for", null) == ID);
            }

            Selected = Node.Attributes
                .FirstOrDefault(item => item.Name == "checked") != null;
        }

        /// <summary>
        /// The Label text for this Option.
        /// </summary>
        public override string Label => LabelNode?.InnerText;

        /// <summary>
        /// The Value used for creating the Submission form, defaults to "on" if selected and no Value attribute exists.
        /// </summary>
        public override string TransmitValue => !string.IsNullOrWhiteSpace(Value) ? Value : Selected ? "on" : null;

        /// <summary>
        /// The Underlying node for this option's Label.
        /// </summary>
        public readonly HtmlNode LabelNode;
    }
}