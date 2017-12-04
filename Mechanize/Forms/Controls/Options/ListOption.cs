using HtmlAgilityPack;

namespace Mechanize.Forms.Controls.Options
{
    public abstract class ListOption
    {
        internal ListOption(ListControl Parent, HtmlNode Node)
        {
            this.Parent = Parent;
            this.Node = Node;
        }

        public string ID => Node.GetAttributeValue("id", null);
        public virtual string Name => Node.InnerText;
        public virtual string Value => Node.GetAttributeValue("value", null);
        public virtual string Label => Node.GetAttributeValue("label", null);

        public virtual string TransmitValue => Value;
        public virtual bool Selected { get; set; }

        protected readonly HtmlNode Node;
        public readonly ListControl Parent;
    }
}