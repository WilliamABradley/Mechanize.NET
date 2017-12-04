using HtmlAgilityPack;
using Mechanize.Forms;
using Mechanize.Requests;
using System;
using System.Threading.Tasks;

namespace Mechanize
{
    public class WebPage
    {
        internal static async Task<WebPage> Create(WebBrowser Browser, WebPageRequestInfo RequestInfo)
        {
            var page = new WebPage(Browser, RequestInfo);
            await page.LoadAsync();
            return page;
        }

        private WebPage(WebBrowser Browser, WebPageRequestInfo RequestInfo)
        {
            SourceBrowser = Browser;
            this.RequestInfo = RequestInfo;
        }

        /// <summary>
        /// Loads all of the Page data, and starts populating the Html.
        /// </summary>
        private async Task LoadAsync()
        {
            // Clears out old data.
            _Forms = null;

            using (var response = await RequestInfo.RequestAsync(SourceBrowser))
            {
                var html = await response.Content.ReadAsStringAsync();
                IsHtml = !string.IsNullOrWhiteSpace(html);
                if (IsHtml)
                {
                    Document = new HtmlDocument();
                    Document.LoadHtml(html);
                }
            }
        }

        /// <summary>
        /// This reloads the Web Page statefully, it remembers all of the original posted Form Data, if any.
        /// </summary>
        /// <returns></returns>
        public Task ReloadAsync()
        {
            return LoadAsync();
        }

        /// <summary>
        /// Provides a Dictionary to get all forms, although not all Forms are available this way, such as ones with No Name, or duplicates. <para/>
        /// To access all available forms, with duplicates and nameless ones, use <see cref="FormCollection.AllForms"/>.
        /// </summary>
        public FormCollection Forms
        {
            get
            {
                if (_Forms == null)
                {
                    _Forms = new FormCollection(this, Document);
                }
                return _Forms;
            }
        }

        private FormCollection _Forms;

        public WebPageRequestInfo RequestInfo { get; }

        public bool IsHtml { get; private set; }

        public bool IsCurrentPage => SourceBrowser.CurrentPage == this;

        public HtmlDocument Document { get; private set; }

        public readonly WebBrowser SourceBrowser;
    }
}