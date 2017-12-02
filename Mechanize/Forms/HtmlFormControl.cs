using HtmlAgilityPack;
using Mechanize.Forms.Controls;
using System;

namespace Mechanize.Forms
{
    public abstract class HtmlFormControl
    {
        public HtmlFormControl(HtmlForm Form, HtmlNode Node)
        {
            this.Form = Form;
            this.Node = Node;
            Type = GetControlType(Node);
        }

        public string ID => Node.Id;
        public bool Disabled => Node.GetAttributeValue("disabled", false);
        public bool ReadOnly => Node.GetAttributeValue("readonly", false);

        public string Value
        {
            get => GetAttribute("value");
            set => SetAttribute("value", value);
        }

        public string GetAttribute(string Attribute)
        {
            return Node.GetAttributeValue(Attribute, null);
        }

        public virtual void SetAttribute(string Attribute, string Value)
        {
            if (Disabled) throw new Exception($"Control {Value ?? Node.Name} is disabled");
            else if (ReadOnly) throw new Exception($"Control {Value ?? Node.Name} is readonly");

            Node.SetAttributeValue(Attribute, Value);
        }

        public readonly FormControlType Type;
        protected readonly HtmlNode Node;

        protected readonly HtmlForm Form;

        internal static FormControlType GetControlType(HtmlNode Node)
        {
            switch (Node.Name.ToLower())
            {
                case "select":
                    return FormControlType.Select;

                case "button":
                    return FormControlType.Submit;

                default:
                    return (FormControlType)Enum.Parse(typeof(FormControlType), Node.GetAttributeValue("type", "Unknown"), true);
            }
        }
    }
}