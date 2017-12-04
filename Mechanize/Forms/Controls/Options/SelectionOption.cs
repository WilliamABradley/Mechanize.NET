using HtmlAgilityPack;

namespace Mechanize.Forms.Controls.Options
{
    public class SelectionOption : ListOption
    {
        internal SelectionOption(ListControl Parent, HtmlNode Node) : base(Parent, Node)
        {
        }

        public override string TransmitValue =>
            !string.IsNullOrWhiteSpace(Value) ? Value :
            !string.IsNullOrWhiteSpace(Label) ? Label :
            Name;

        public override bool Selected
        {
            get => base.Selected;
            set
            {
                if (value == true)
                {
                    foreach (var option in Parent.Options)
                    {
                        if (option != this)
                        {
                            option.Selected = false;
                        }
                    }
                }
                base.Selected = value;
            }
        }
    }
}