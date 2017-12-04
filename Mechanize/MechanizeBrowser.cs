using Mechanize.Exceptions;
using Mechanize.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mechanize
{
    /// <summary>
    /// The Mechanize Browser for .NET, used for Navigating Statefully, and collecting and manipulating forms.
    /// </summary>
    public class MechanizeBrowser : IDisposable
    {
        /// <summary>
        /// Constructor for <see cref="MechanizeBrowser"/>.
        /// </summary>
        /// <param name="Timeout">The Custom Timeout that the Client should wait for concluding that a page has failed to load.</param>
        public MechanizeBrowser(TimeSpan? Timeout = null)
        {
        }

        /// <summary>
        /// Navigates to a new Web Page using GET, given the provided Uri, and optional Data.
        /// </summary>
        /// <param name="Uri">Page to Navigate to.</param>
        /// <returns>Navigated Web Page.</returns>
        public Task<WebPage> NavigateAsync(string Uri)
        {
            return NavigateAsync(new Uri(Uri));
        }

        /// <summary>
        /// Navigates to a new Web Page using GET, given the provided Uri, and optional Data.
        /// </summary>
        /// <param name="Uri">Page to Navigate to.</param>
        /// <returns>Navigated Web Page.</returns>
        public Task<WebPage> NavigateAsync(Uri Uri)
        {
            return NavigateAsync(new WebPageRequestInfo(Uri));
        }

        /// <summary>
        /// Navigates to a new Web Page, given the provided Request Information.
        /// </summary>
        /// <param name="Info">Request Information.</param>
        /// <returns>Navigated Web Page.</returns>
        public async Task<WebPage> NavigateAsync(WebPageRequestInfo Info)
        {
            if (CurrentPage?.RequestInfo?.UpdateHistory == true) _History.Add(CurrentPage);
            CurrentPage = await WebPage.Create(this, Info);
            return CurrentPage;
        }

        /// <summary>
        /// Navigates up the History Stack, if possible.
        /// </summary>
        /// <param name="Steps">How many Pages backward to go.</param>
        /// <exception cref="MechanizeBrowserStateException">Already at start of History</exception>
        /// <returns>Back Page</returns>
        public WebPage GoBack(int Steps = 1)
        {
            while (Steps > 0 || CurrentPage == null)
            {
                try
                {
                    CurrentPage = History.Last();
                    _History.Remove(CurrentPage);
                    _ForwardHistory.Add(CurrentPage);
                }
                catch (InvalidOperationException)
                {
                    throw new MechanizeBrowserStateException("Already at start of History");
                }
            }
            return CurrentPage;
        }

        /// <summary>
        /// Navigates up the Forward Stack, if possible.
        /// </summary>
        /// <param name="Steps">How many Pages forward to go.</param>
        /// <exception cref="MechanizeBrowserStateException">Can't go Forward any further</exception>
        /// <returns>Forward Page</returns>
        public WebPage GoForward(int Steps = 1)
        {
            while (Steps > 0 || CurrentPage == null)
            {
                try
                {
                    CurrentPage = ForwardHistory.Last();
                    _ForwardHistory.Remove(CurrentPage);
                    _History.Add(CurrentPage);
                }
                catch (InvalidOperationException)
                {
                    throw new MechanizeBrowserStateException("Can't go Forward any further");
                }
            }
            return CurrentPage;
        }

        /// <summary>
        /// Clears both Forward and History Stacks.
        /// </summary>
        public void ClearHistory()
        {
            foreach (var item in History.Reverse())
            {
                _History.Remove(item);
            }

            foreach (var item in ForwardHistory.Reverse())
            {
                _ForwardHistory.Remove(item);
            }
        }

        /// <summary>
        /// Disposes of the Internal Client, and Client Handler.
        /// </summary>
        public void Dispose()
        {
            Client?.Dispose();
            ClientHandler?.Dispose();
            Disposed = true;
        }

        /// <summary>
        /// The Current Page that the Browser is Navigated to.
        /// </summary>
        public WebPage CurrentPage { get; private set; }

        /// <summary>
        /// Determines if the Browser can go Back a Page.
        /// </summary>
        public bool CanGoBack => History.Any();

        /// <summary>
        /// Determines if the Browser can go Forward a Page.
        /// </summary>
        public bool CanGoForward => ForwardHistory.Any();

        /// <summary>
        /// The History Stack, for when NavigateAsync is used more than once, the current page gets added to the History stack.
        /// </summary>
        public IReadOnlyList<WebPage> History => _History;

        private List<WebPage> _History = new List<WebPage>();

        /// <summary>
        /// The Forward History Stack, for when the <see cref="GoBack(int)"/> function is used, the current page gets added to the forward stack.
        /// </summary>
        public IReadOnlyList<WebPage> ForwardHistory => _ForwardHistory;

        private List<WebPage> _ForwardHistory = new List<WebPage>();

        /// <summary>
        /// The Cookies for this Session.
        /// </summary>
        public CookieContainer Cookies { get; } = new CookieContainer();

        /// <summary>
        /// The Internal Client for handling Page Operations.
        /// </summary>
        internal HttpClient Client
        {
            get
            {
                if (Disposed || _Client == null)
                {
                    // Ensures that the old Instances are disposed of, if they exist.
                    ClientHandler?.Dispose();
                    _Client?.Dispose();

                    // Creates the Handler for Controlling Cookies.
                    ClientHandler = new HttpClientHandler
                    {
                        CookieContainer = Cookies
                    };
                    _Client = new HttpClient(ClientHandler);
                    if (TimeOut.HasValue)
                    {
                        _Client.Timeout = TimeOut.Value;
                    }
                    if (!string.IsNullOrWhiteSpace(UserAgent))
                    {
                        _Client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
                    }
                    Disposed = false;
                }
                return _Client;
            }
        }

        private HttpClient _Client;

        /// <summary>
        /// The Handler for the HttpClient, use it to configure any custom settings for the internal Client.
        /// </summary>
        public HttpClientHandler ClientHandler { get; private set; }

        /// <summary>
        /// The User Agent for the Browser to mimic.
        /// </summary>
        public string UserAgent
        {
            get => _UserAgent;
            set
            {
                _UserAgent = value;
                if (!Disposed && _Client != null)
                {
                    _Client.DefaultRequestHeaders.Add("User-Agent", _UserAgent);
                }
            }
        }

        private string _UserAgent;

        /// <summary>
        /// The Custom Timeout that the Client should wait for concluding that a page has failed to load.
        /// </summary>
        public TimeSpan? TimeOut
        {
            get => _TimeOut;
            set
            {
                _TimeOut = value;
                if (_TimeOut.HasValue && !Disposed && _Client != null)
                {
                    Client.Timeout = _TimeOut.Value;
                }
            }
        }

        private TimeSpan? _TimeOut;

        /// <summary>
        /// Determines the state of the Internal Client and Handler.
        /// </summary>
        private bool Disposed = false;
    }
}