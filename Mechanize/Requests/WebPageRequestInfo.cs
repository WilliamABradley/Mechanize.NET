// ******************************************************************
// Copyright (c) William Bradley
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

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
        /// <param name="UpdateHistory">Determines whether to add this Request to the Back/Forward stack.</param>
        public WebPageRequestInfo(Uri Address, bool UpdateHistory) : this(Address, Data: null, UpdateHistory: UpdateHistory)
        {
        }

        /// <summary>
        /// Constructs an Http Request for a Web Page.
        /// </summary>
        /// <param name="Address">Address of the Web Page</param>
        /// <param name="Data">The Data to Request this page with.</param>
        /// <param name="Method">The HttpMethod that this Page will be requested with.</param>
        /// <param name="EncodingType">The Encoding Type that this Page will be Requested with.</param>
        /// <param name="UpdateHistory">Determines whether to add this Request to the Back/Forward stack.</param>
        public WebPageRequestInfo(Uri Address, List<KeyValuePair<string, IRequestInfo>> Data = null, string Method = "GET", string EncodingType = URLEncoded, bool UpdateHistory = true)
        {
            this.Address = Address;
            this.Data = Data;
            this.Method = Method.ToUpper();
            this.EncodingType = EncodingType;
            this.UpdateHistory = UpdateHistory;
        }

        /// <summary>
        /// Requests a Response with the Provided Submission Data.
        /// </summary>
        /// <param name="Browser">Browser used to Request, and Navigate with.</param>
        /// <returns>The Response Message.</returns>
        /// <exception cref="MechanizeBrowserStateException">No Content to Post, can't discern Encoding Type.</exception>
        internal async Task<HttpResponseMessage> RequestAsync(MechanizeBrowser Browser)
        {
            HttpResponseMessage message = null;
            var client = Browser.Client;

            // Creates a POST Request.
            if (Method == "POST")
            {
                if (Data == null)
                {
                    throw new MechanizeBrowserStateException("There was no content to POST");
                }

                // Creates the HttpContent to Post, from the provided Encoding Type.
                var Content =
                    EncodingType == URLEncoded ? CreateUrlEncoded() :
                    EncodingType == MultiPart ? CreateMultiForm() :
                    null;

                if (Content == null)
                {
                    throw new MechanizeBrowserStateException("Could not discern Encoding Type");
                }

                using (Content)
                {
                    // Posts the Data.
                    message = await client.PostAsync(Address, Content);
                }
            }
            // Creates a GET Request.
            else message = await client.GetAsync(CreateGetRequest());

            EvaluatedAddress = message.RequestMessage.RequestUri;
            return message;
        }

        /// <summary>
        /// Creates UrlEncoded Form Content.
        /// </summary>
        /// <returns>FormUrlEncodedContent</returns>
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

        /// <summary>
        /// Creates MultPart/FormData Content.
        /// </summary>
        /// <returns>MultipartFormDataContent</returns>
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

        /// <summary>
        /// Creates the GET Request Uri with Query String.
        /// </summary>
        /// <returns>The GET Uri</returns>
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

        /// <summary>
        /// The HttpMethod to Request with.
        /// </summary>
        public readonly string Method;

        /// <summary>
        /// The state data for the Web Page.
        /// </summary>
        public readonly IReadOnlyList<KeyValuePair<string, IRequestInfo>> Data;

        /// <summary>
        /// Determines whether to add this Request to the Back/Forward stack.
        /// </summary>
        public readonly bool UpdateHistory;

        /// <summary>
        /// The Encoding Type that this Page will be Requested with.
        /// </summary>
        public readonly string EncodingType;
    }
}