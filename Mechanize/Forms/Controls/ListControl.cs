using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Mechanize.Forms.Controls.Options;
using System.Linq;

namespace Mechanize.Forms.Controls
{
    public abstract class ListControl : HtmlFormControl
    {
        public ListControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
        }

        public void Select(string Value)
        {
            var option = FindOption(Value);
            if (option == null)
            {
                throw new Exception("This is not an option for this form");
            }
            Select(option);
        }

        public void Select(ListOption Option)
        {
            if (Option.Parent != this)
            {
                throw new Exception("This is not an option for this form");
            }
            Option.Selected = true;
        }

        public ListOption FindOption(string Value)
        {
            return Options.FirstOrDefault(item =>
                Value.Equals(item.Value, StringComparison.InvariantCultureIgnoreCase)
                || Value.Equals(item.Label, StringComparison.InvariantCultureIgnoreCase)
                || Value.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase));
        }

        public abstract IReadOnlyList<ListOption> Options { get; }

        protected List<ListOption> _Options;
    }
}