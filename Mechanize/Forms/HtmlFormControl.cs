using HtmlAgilityPack;
using Mechanize.Forms.Controls;
using Mechanize.Requests;
using System;
using System.Collections.Generic;

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

        public string Name
        {
            get => GetAttribute("name");
        }

        public virtual string Value
        {
            get => GetAttribute("value");
            set => SetAttribute("value", value);
        }

        public string GetAttribute(string Attribute)
        {
            return Node.GetAttributeValue(Attribute, null);
        }

        public string GetAttribute(string Attribute, string Default)
        {
            return GetAttribute(Attribute) ?? Default;
        }

        public virtual void SetAttribute(string Attribute, string Value)
        {
            CheckSetAttribute();
            Node.SetAttributeValue(Attribute, Value);
        }

        protected void CheckSetAttribute()
        {
            if (Disabled) throw new Exception($"Control {Value ?? Node.Name} is disabled");
            else if (ReadOnly) throw new Exception($"Control {Value ?? Node.Name} is readonly");
        }

        internal virtual List<IRequestInfo> GetRequestInfo()
        {
            return new List<IRequestInfo> { new StringRequestInfo(Value) };
        }

        public readonly FormControlType Type;
        public readonly HtmlNode Node;

        internal readonly HtmlForm Form;

        internal static FormControlType GetControlType(HtmlNode Node)
        {
            var type = GetControlType(Node.Name.ToLower());
            if (type == FormControlType.Unknown)
            {
                var typeattr = Node.GetAttributeValue("type", "text");
                type = GetControlType(typeattr);
                if (type == FormControlType.Unknown)
                {
                    type = (FormControlType)Enum.Parse(typeof(FormControlType), typeattr, true);
                }
            }
            return type;
        }

        internal static FormControlType GetControlType(string Type)
        {
            switch (Type)
            {
                case "select":
                    return FormControlType.Select;

                case "button":
                case "buttonbutton":
                case "reset":
                case "resetbutton":
                    return FormControlType.Ignore;

                case "radio":
                    return FormControlType.Radio;

                case "checkbox":
                    return FormControlType.Checkbox;

                case "image":
                    return FormControlType.Image;

                case "file":
                    return FormControlType.File;

                case "password":
                    return FormControlType.Password;

                case "textarea":
                    return FormControlType.TextArea;

                case "text":
                    return FormControlType.Text;

                case "submit":
                case "submitbutton":
                    return FormControlType.Submit;

                case "hidden":
                    return FormControlType.Hidden;

                default:
                    return FormControlType.Unknown;
            }
        }
    }
}