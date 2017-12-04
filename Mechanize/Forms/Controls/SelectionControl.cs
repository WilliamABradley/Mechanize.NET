using HtmlAgilityPack;
using Mechanize.Forms.Controls.Options;
using System.Collections.Generic;
using System.Linq;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// Covers: SELECT(and OPTION) <para/>
    /// SELECT control values and labels are subject to some messy defaulting rules. <para/>
    /// </summary>
    public class SelectionControl : ListControl
    {
        internal SelectionControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
            Options.First().Selected = true;
        }

        public ListOption Selected => Options.FirstOrDefault(item => item.Selected);

        public override string Value
        {
            get => Selected.TransmitValue;
            set
            {
                CheckSetAttribute();
                Select(value);
            }
        }

        public override IReadOnlyList<ListOption> Options
        {
            get
            {
                if (_Options == null)
                {
                    var options = Node.Descendants().Where(item => item.Name.ToLower() == "option");
                    _Options = options.Select(item => (ListOption)new SelectionOption(this, item)).ToList();
                }

                return _Options;
            }
        }
    }
}