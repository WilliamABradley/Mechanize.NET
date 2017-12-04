using HtmlAgilityPack;

namespace Mechanize.Forms.Controls.Options
{
    public class RadioOption : CheckBoxOption
    {
        internal RadioOption(ListControl Parent, HtmlNode Node) : base(Parent, Node)
        {
        }

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