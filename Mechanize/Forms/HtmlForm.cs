using HtmlAgilityPack;
using Mechanize.Forms.Controls;
using System.Collections.Generic;
using System.Linq;

namespace Mechanize.Forms
{
    public class HtmlForm : List<HtmlFormControl>
    {
        internal HtmlForm(WebPage SourcePage, HtmlNode FormNode)
        {
            this.SourcePage = SourcePage;
            this.FormNode = FormNode;
            Name = FormNode.GetAttributeValue("name", null);

            AddControls();
        }

        private void AddControls()
        {
            foreach (var control in FormNode.Descendants()
                .Where(item =>
                    {
                        var name = item.Name.ToLower();
                        return name == "select"
                                    || name == "input"
                                    || name == "button";
                    }
                ))
            {
                switch (HtmlFormControl.GetControlType(control))
                {
                    case FormControlType.Text:
                        Add(new TextInputControl(this, control));
                        break;

                    case FormControlType.Submit:
                        Add(new SubmitControl(this, control));
                        break;
                }
            }
        }

        public string Name { get; }

        private readonly HtmlNode FormNode;
        internal readonly WebPage SourcePage;
    }
}