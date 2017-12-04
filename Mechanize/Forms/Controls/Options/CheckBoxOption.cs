using HtmlAgilityPack;
using System.Linq;

namespace Mechanize.Forms.Controls.Options
{
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

        public override string Name => Node.GetAttributeValue("name", null);
        public override string Label => LabelNode?.InnerText;

        public override string TransmitValue => !string.IsNullOrWhiteSpace(Value) ? Value : Selected ? "on" : null;

        private readonly HtmlNode LabelNode;
    }
}