using HtmlAgilityPack;
using Mechanize.Forms;
using Mechanize.Requests;
using System.Threading.Tasks;

namespace Mechanize
{
    /// <summary>
    /// A WebPage that is the Result of a call to <see cref="MechanizeBrowser.NavigateAsync(WebPageRequestInfo)"/>, or as a result of a Form Submission. <para/>
    /// Any state information can be found in <see cref="RequestInfo"/>, and is used to reload the page with the same state. Although any form data changed on this page will be lost.
    /// </summary>
    public class WebPage
    {
        internal static async Task<WebPage> Create(MechanizeBrowser Browser, WebPageRequestInfo RequestInfo)
        {
            var page = new WebPage(Browser, RequestInfo);
            await page.LoadAsync();
            return page;
        }

        private WebPage(MechanizeBrowser Browser, WebPageRequestInfo RequestInfo)
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
        /// This reloads the Web Page statefully, it remembers all of the original posted Form Data, if any. <para/>
        /// Any modified form data belonging to this page will be lost.
        /// </summary>
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

        /// <summary>
        /// The Request Information that was used to Load the Page, as well as reloading the Page.
        /// </summary>
        public WebPageRequestInfo RequestInfo { get; }

        /// <summary>
        /// Determines if the current Page is Html, if not, <see cref="Document"/> will be null.
        /// </summary>
        public bool IsHtml { get; private set; }

        /// <summary>
        /// Determines if this Page is the Current Page for the Browser.
        /// </summary>
        public bool IsCurrentPage => SourceBrowser.CurrentPage == this;

        /// <summary>
        /// The Html for this Page, Call <see cref="ReloadAsync"/> to load this page again, with the same state.
        /// </summary>
        public HtmlDocument Document { get; private set; }

        /// <summary>
        /// The Browser that this WebPage belongs to.
        /// </summary>
        public readonly MechanizeBrowser SourceBrowser;
    }
}