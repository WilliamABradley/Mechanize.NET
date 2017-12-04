using HtmlAgilityPack;

namespace Mechanize.Forms.Controls.Options
{
    /// <summary>
    /// An option pertaining to a <see cref="CheckBoxControl"/>.
    /// </summary>
    public class RadioOption : CheckBoxOption
    {
        internal RadioOption(ListControl Parent, HtmlNode Node) : base(Parent, Node)
        {
        }

        /// <summary>
        /// Determines if the current Option is selected, if Setting to true, it will deselect all other options, as a <see cref="CheckBoxControl"/> only supports one Selection.
        /// </summary>
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