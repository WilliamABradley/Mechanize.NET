using HtmlAgilityPack;
using Mechanize.Requests;
using System;
using System.Collections.Generic;

namespace Mechanize.Forms.Controls
{
    public class FileControl : HtmlFormControl
    {
        internal FileControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
        }

        public void SelectFile(byte[] FileData, string FileName = null)
        {
            this.FileData = FileData;
            this.FileName = FileName;
        }

        internal override List<IRequestInfo> GetRequestInfo()
        {
            if (HasFile) return new List<IRequestInfo> { new FileRequestInfo(FileData, FileName) };
            else return null;
        }

        public bool HasFile => FileData != null;

        public string FileName { get; private set; }

        public byte[] FileData { get; private set; }

        public override string Value { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
    }
}