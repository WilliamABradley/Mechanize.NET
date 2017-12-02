using HtmlAgilityPack;
using Mechanize.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mechanize
{
    public class WebPage : IDisposable
    {
        internal static async Task<WebPage> Create(Uri Address, HttpResponseMessage Response)
        {
            var html = await Response.Content.ReadAsStringAsync();
            return new WebPage(Address, Response, html);
        }

        private WebPage(Uri Address, HttpResponseMessage Response, string html)
        {
            this.Address = Address;

            IsHtml = !string.IsNullOrWhiteSpace(html);
            if (IsHtml)
            {
                Document = new HtmlDocument();
                Document.LoadHtml(html);
            }
        }

        public void Dispose()
        {
        }

        public void Reload()
        {
        }

        public IReadOnlyDictionary<string, HtmlForm> Forms
        {
            get
            {
                if (_Forms == null)
                {
                    var forms = Document.DocumentNode.Descendants()?.Where(item => item.Name == "form");
                    _Forms = forms
                        .Select(form => new HtmlForm(this, form))
                        .ToDictionary(form => form.Name, form => form);
                }

                return _Forms;
            }
        }

        private Dictionary<string, HtmlForm> _Forms;

        public Uri Address { get; }

        public bool IsHtml { get; }

        private readonly HtmlDocument Document;
    }
}