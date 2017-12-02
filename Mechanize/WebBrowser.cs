using Mechanize.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mechanize
{
    public class WebBrowser : IDisposable
    {
        public WebBrowser(TimeSpan? Timeout = null, bool AllowXHtml = false)
        {
            this.AllowXHtml = AllowXHtml;
            Client = new HttpClient();
            if (Timeout != null)
            {
                Client.Timeout = Timeout.Value;
            }
        }

        public async Task<WebPage> Open(Uri Uri, Dictionary<string, string> Data = null)
        {
            return await MechanizeOpen(Uri, Data);
        }

        private async Task<WebPage> MechanizeOpen(Uri Uri, Dictionary<string, string> Data = null, bool UpdateHistory = true, bool Visit = true)
        {
            using (var request = new HttpRequestMessage(Data != null ? HttpMethod.Post : HttpMethod.Get, Uri))
            {
                using (var response = await Client.SendAsync(request))
                {
                    if (CurrentPage != null) _History.Add(CurrentPage);
                    CurrentPage = await WebPage.Create(Uri, response);
                    return CurrentPage;
                }
            }
        }

        public WebPage GoBack(int Steps = 1)
        {
            CurrentPage?.Dispose();
            while (Steps > 0 || CurrentPage == null)
            {
                try
                {
                    CurrentPage = History.Last();
                    _History.Remove(CurrentPage);
                }
                catch (InvalidOperationException)
                {
                    throw new BrowserStateException("Already at start of History");
                }
            }
            return CurrentPage;
        }

        public WebPage GoForward(int Steps = 1)
        {
            CurrentPage?.Dispose();
            while (Steps > 0 || CurrentPage == null)
            {
                try
                {
                    CurrentPage = ForwardHistory.Last();
                    _ForwardHistory.Remove(CurrentPage);
                }
                catch (InvalidOperationException)
                {
                    throw new BrowserStateException("Can't go Forward any further");
                }
            }
            return CurrentPage;
        }

        public void ClearHistory()
        {
            foreach (var item in History.Reverse())
            {
                item.Dispose();
                _History.Remove(item);
            }

            foreach (var item in ForwardHistory.Reverse())
            {
                item.Dispose();
                _ForwardHistory.Remove(item);
            }
        }

        public void Dispose()
        {
            Client?.Dispose();
        }

        public WebPage CurrentPage { get; private set; }

        public bool CanGoBack => History.Any();
        public bool CanGoForward => ForwardHistory.Any();

        public IReadOnlyList<WebPage> History => _History;
        private List<WebPage> _History = new List<WebPage>();

        public IReadOnlyList<WebPage> ForwardHistory => _ForwardHistory;
        private List<WebPage> _ForwardHistory = new List<WebPage>();

        public bool AllowXHtml { get; }

        private HttpClient Client { get; }
    }
}