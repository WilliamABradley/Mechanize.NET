using System.Collections.Generic;
using Mechanize.Forms.Controls.Options;
using System.Linq;
using HtmlAgilityPack;

namespace Mechanize.Forms.Controls
{
    public class RadioControl : ListControl
    {
        public RadioControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
            AddOption(Node);
        }

        internal void AddOption(HtmlNode Option)
        {
            _Options.Add(new RadioOption(this, Option));
        }

        public override IReadOnlyList<ListOption> Options => _Options;

        public RadioOption Selected => _Options.Cast<RadioOption>()
            .FirstOrDefault(item => item.Selected);

        public override string Value
        {
            get => Selected.TransmitValue;
            set
            {
                CheckSetAttribute();
                Select(value);
            }
        }
    }
}