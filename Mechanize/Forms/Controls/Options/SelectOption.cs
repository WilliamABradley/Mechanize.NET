using HtmlAgilityPack;

namespace Mechanize.Forms.Controls.Options
{
    /// <summary>
    /// An option pertaining to a <see cref="SelectControl"/>.
    /// </summary>
    public class SelectOption : ListOption
    {
        internal SelectOption(ListControl Parent, HtmlNode Node) : base(Parent, Node)
        {
        }

        /// <summary>
        /// The Value used for creating the Submission form, an amalgamation of Value, Label, and Name, whatever isn't null.
        /// </summary>
        public override string TransmitValue =>
            !string.IsNullOrWhiteSpace(Value) ? Value :
            !string.IsNullOrWhiteSpace(Label) ? Label :
            Name;

        /// <summary>
        /// Determines if the current Option is selected, if Setting to true, it will deselect all other options, as a <see cref="SelectControl"/> only supports one Selection.
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