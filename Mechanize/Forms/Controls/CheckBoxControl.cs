using HtmlAgilityPack;
using Mechanize.Forms.Controls.Options;
using System;
using System.Collections.Generic;
using Mechanize.Requests;
using System.Linq;

namespace Mechanize.Forms.Controls
{
    public class CheckBoxControl : ListControl
    {
        internal CheckBoxControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
            AddOption(Node);
        }

        internal void AddOption(HtmlNode Option)
        {
            _Options.Add(new CheckBoxOption(this, Option));
        }

        public void DeSelect(string Value)
        {
            var option = FindOption(Value);
            if (option == null)
            {
                throw new Exception("This is not an option for this form");
            }
            DeSelect(option);
        }

        public void DeSelect(ListOption Option)
        {
            Option.Selected = false;
        }

        internal override List<IRequestInfo> GetRequestInfo()
        {
            return Options
                .Where(item => ((CheckBoxOption)item).Selected == true)
                .Select(item => (IRequestInfo)new StringRequestInfo(item.TransmitValue)).ToList();
        }

        public override string Value
        {
            get
            {
                return string.Join(";", Selected.Select(item => item.TransmitValue));
            }
            set
            {
                var newoptions = value.Split(';');
                foreach (var option in _Options)
                {
                    option.Selected = false;
                }
                foreach (var option in newoptions)
                {
                    Select(option);
                }
            }
        }

        public override IReadOnlyList<ListOption> Options => _Options;

        public IReadOnlyList<CheckBoxOption> Selected => _Options.Cast<CheckBoxOption>()
            .Where(item => item.Selected)
            .ToList();
    }
}