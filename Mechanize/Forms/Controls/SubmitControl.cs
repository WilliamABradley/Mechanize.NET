using HtmlAgilityPack;
using System;
using System.Drawing;

namespace Mechanize.Forms.Controls
{
    public class SubmitControl : HtmlFormControl, IFormClickable
    {
        internal SubmitControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
        }

        public void Click(Point Coordinates)
        {
            throw new NotImplementedException();
        }

        public bool Clicked { get => _Clicked; internal set => _Clicked = value; }
        private bool _Clicked;
    }
}