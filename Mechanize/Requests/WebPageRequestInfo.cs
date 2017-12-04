using Mechanize.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Mechanize.Requests
{
    public class WebPageRequestInfo
    {
        /// <summary>
        /// The Default Encoding type for a Form. Encodes the data using full string Encoding.
        /// </summary>
        public const string URLEncoded = "application/x-www-form-urlencoded";

        /// <summary>
        /// Uses Multipart Encoding, allowing for file uploads.
        /// </summary>
        public const string MultiPart = "multipart/form-data";

        /// <summary>
        /// Constructs a GET Request for a Web Page.
        /// </summary>
        /// <param name="Address">Address of the Web Page to GET</param>
        /// <param name="UpdateHistory"></param>
        /// <param name="Visit"></param>
        public WebPageRequestInfo(Uri Address, bool UpdateHistory) : this(Address, Data: null, UpdateHistory: UpdateHistory)
        {
        }

        public WebPageRequestInfo(Uri Address, List<KeyValuePair<string, IRequestInfo>> Data = null, string Method = "GET", string EncodingType = URLEncoded, bool UpdateHistory = true)
        {
            this.Address = Address;
            this.Data = Data;
            this.Method = Method.ToUpper();
            this.EncodingType = EncodingType;
            this.UpdateHistory = UpdateHistory;
        }

        internal async Task<HttpResponseMessage> RequestAsync(WebBrowser Browser)
        {
            HttpResponseMessage message = null;
            var client = Browser.Client;

            if (Method == "POST")
            {
                if (Data == null)
                {
                    throw new BrowserStateException("There was no content to POST");
                }

                var Content =
                    EncodingType == URLEncoded ? CreateUrlEncoded() :
                    EncodingType == MultiPart ? CreateMultiForm() :
                    null;

                if (Content == null)
                {
                    throw new BrowserStateException("Could not discern Encoding Type");
                }

                using (Content)
                {
                    message = await client.PostAsync(Address, Content);
                }
            }
            else message = await client.GetAsync(CreateGetRequest());

            EvaluatedAddress = message.RequestMessage.RequestUri;
            return message;
        }

        private HttpContent CreateUrlEncoded()
        {
            var encodedData = new Dictionary<string, string>();
            foreach (var item in Data)
            {
                if (item.Value is StringRequestInfo str)
                {
                    encodedData.Add(item.Key, str.Value);
                }
            }
            return new FormUrlEncodedContent(encodedData);
        }

        private HttpContent CreateMultiForm()
        {
            var multipartForm = new MultipartFormDataContent();
            // add forms parts
            foreach (var item in Data)
            {
                if (item.Value is StringRequestInfo str)
                {
                    if (string.IsNullOrWhiteSpace(str.Value)) continue;
                    multipartForm.Add(new StringContent(str.Value), item.Key);
                }
                else if (item.Value is FileRequestInfo file)
                {
                    multipartForm.Add(new ByteArrayContent(file.FileData), item.Key, file.FileName);
                }
            }
            return multipartForm;
        }

        private Uri CreateGetRequest()
        {
            var uri = Address;
            if (Data != null)
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                foreach (var item in Data)
                {
                    if (item.Value is StringRequestInfo str)
                    {
                        query.Add(item.Key, str.Value);
                    }
                }

                var builder = new UriBuilder(Address) { Query = query.ToString() };
                uri = builder.Uri;
            }
            return uri;
        }

        /// <summary>
        /// The original Address part, contains no state data.
        /// </summary>
        public readonly Uri Address;

        /// <summary>
        /// The final address, after any Redirection.
        /// </summary>
        public Uri EvaluatedAddress { get; private set; }

        public readonly string Method;

        public readonly IReadOnlyList<KeyValuePair<string, IRequestInfo>> Data;

        public readonly bool UpdateHistory;

        public readonly string EncodingType;
    }
}