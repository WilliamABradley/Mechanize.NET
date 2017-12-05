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

using Mechanize.Forms.Controls;
using Mechanize.Html;
using Mechanize.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mechanize.Forms
{
    /// <summary>
    /// Represents a single Html Form element for a WebPage. <para/>
    /// A form consists of a sequence of controls that usually have names, and
    /// which can take on various values.The values of the various types of
    /// controls represent variously: text, zero-or-one-of-many or many-of-many
    /// choices, and files to be uploaded.Some controls can be clicked on to
    /// submit the form. <para/>
    /// Forms can be filled in with data to be returned to the server, and then submitted, using the <see cref="SubmitForm"/>
    /// method. Or via collecting the Submission data with <see cref="GetSubmissionData"/>.
    /// </summary>
    public class HtmlForm : IReadOnlyList<HtmlFormControl>
    {
        internal HtmlForm(WebPage SourcePage, IHtmlNode Node)
        {
            this.SourcePage = SourcePage;
            this.Node = Node;
            Name = Node.GetAttribute("name", null);

            AddControls();
        }

        /// <summary>
        /// Finds a Form Control that matches the provided name.
        /// </summary>
        /// <param name="Name">Name of the Control to find.</param>
        /// <returns>The control with matching name.</returns>
        public HtmlFormControl FindControl(string Name)
        {
            return FindControl<HtmlFormControl>(Name);
        }

        /// <summary>
        /// Finds a Form Control that matches the provided name.
        /// </summary>
        /// <typeparam name="T">An HtmlFormControl Type.</typeparam>
        /// <param name="Name">Name of the Control to find.</param>
        /// <returns>The control with matching name.</returns>
        public T FindControl<T>(string Name) where T : HtmlFormControl
        {
            return (T)this.FirstOrDefault(item => item.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Collects the Descendant nodes, and tries to match them to a form control, then add them to the form.
        /// </summary>
        private void AddControls()
        {
            foreach (var control in Node.Descendants
                .Where(item =>
                {
                    var name = item.Name.ToLower();
                    // Only checks the valid form Tags.
                    return name == "select"
                                || name == "input"
                                || name == "button";
                }
                ))
            {
                // Determines and adds the Control to the List.
                GetFormControl(control);
            }
        }

        /// <summary>
        /// Creates the Wrapper Class around potentially Valid Form Controls.
        /// </summary>
        /// <param name="control">Node to determine the control it relates to.</param>
        private void GetFormControl(IHtmlNode control)
        {
            var name = control.GetAttribute("name", null);
            switch (HtmlFormControl.GetControlType(control))
            {
                // Text Input Aliases.
                case FormControlType.Text:
                case FormControlType.TextArea:
                case FormControlType.Password:
                case FormControlType.Hidden:
                    _List.Add(new TextInputControl(this, control));
                    break;

                case FormControlType.Image:
                    _List.Add(new ImageControl(this, control));
                    break;

                case FormControlType.Submit:
                    _List.Add(new SubmitControl(this, control));
                    break;

                case FormControlType.Ignore:
                    _List.Add(new ButtonControl(this, control));
                    break;

                case FormControlType.File:
                    _List.Add(new FileControl(this, control));
                    break;

                case FormControlType.Select:
                    _List.Add(new SelectControl(this, control));
                    break;

                case FormControlType.Radio:
                    // Finds the Radio group, if exists join, if not, create new Radio Group.
                    var radio = _List.OfType<RadioControl>().FirstOrDefault(item => item.Name == name);

                    if (radio != null) radio.AddOption(control);
                    else _List.Add(new RadioControl(this, control));
                    break;

                case FormControlType.Checkbox:
                    // Finds the Checkbox group, if exists join, if not, create new Checkbox Group.
                    var checkbox = _List.OfType<CheckBoxControl>().FirstOrDefault(item => item.Name == name);

                    if (checkbox != null) checkbox.AddOption(control);
                    else _List.Add(new CheckBoxControl(this, control));
                    break;
            }
        }

        /// <summary>
        /// Submits the Form using the first available submit Button.
        /// </summary>
        /// <returns>New navigated page</returns>
        public Task<WebPage> SubmitForm()
        {
            return SubmitForm(this.OfType<SubmitControl>().First());
        }

        /// <summary>
        /// Submits the Form, with the provided submit button as the target.
        /// </summary>
        /// <param name="SubmitButton">Button to Submit with, if Required.</param>
        /// <returns></returns>
        public Task<WebPage> SubmitForm(SubmitControl SubmitButton)
        {
            var address = new Uri(Action, UriKind.RelativeOrAbsolute);
            // Attaches the Absolute address from the current page, if the address isn't already absolute.
            if (!address.IsAbsoluteUri)
            {
                address = new Uri(SourcePage.RequestInfo.Address, address);
            }

            var data = GetSubmissionData(SubmitButton);

            // Populates the Web Page Request to Navigate the Browser along.
            var requestInfo = new WebPageRequestInfo(
                address,
                data,
                Method.ToUpper(),
                EncodingType);

            return SourcePage.SourceBrowser.NavigateAsync(requestInfo);
        }

        /// <summary>
        /// Gets the submission data for submitting the form, as if using the first available submit button.
        /// </summary>
        /// <returns>The Form's Submission Data</returns>
        public List<KeyValuePair<string, IRequestInfo>> GetSubmissionData()
        {
            return GetSubmissionData(this.OfType<SubmitControl>().First());
        }

        /// <summary>
        /// Gets the submission data for submitting the form, as if using the first available submit button.
        /// </summary>
        /// <param name="SubmitButton">Button to Submit with, if Required.</param>
        /// <returns>The Form's Submission Data</returns>
        public List<KeyValuePair<string, IRequestInfo>> GetSubmissionData(SubmitControl SubmitButton)
        {
            var entries = new List<KeyValuePair<string, IRequestInfo>>();

            var items = this.Where(item =>
                !string.IsNullOrWhiteSpace(item.Name)
                && item.Type != FormControlType.Ignore
                && (item.Type != FormControlType.Submit
                        // add the Submit button to the Submission data, only if there is more than one submit control.
                        || (item == SubmitButton && this.OfType<SubmitControl>().Count() > 1)));

            foreach (var item in items)
            {
                var requests = item.GetRequestInfo();
                // If there are no requests, continue. This can occur with the FileControl and CheckBox/Radio Controls.
                if (requests == null) continue;
                // Adds all the various requests, multiple requests can occur with Checkboxes.
                foreach (var request in requests)
                {
                    entries.Add(new KeyValuePair<string, IRequestInfo>(item.Name, request));
                }
            }

            return entries;
        }

        /// <summary>
        /// The name of the Current Form.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Represents the Uri that the Form will use to Submit the form results to.
        /// </summary>
        public string Action
        {
            get => Node.GetAttribute("action", null);
        }

        /// <summary>
        /// Represents the <see cref="System.Net.Http.HttpMethod"/> that the Form Requests to submit the form.
        /// </summary>
        public string Method
        {
            get => Node.GetAttribute("method", "GET");
        }

        /// <summary>
        /// Represents the Encoding Type that the Form requests on submission.
        /// </summary>
        public string EncodingType
        {
            get => Node.GetAttribute("enctype", WebPageRequestInfo.URLEncoded);
        }

        /// <summary>
        /// Represents the Action the Page would take on Submission.
        /// </summary>
        public string OnSubmit
        {
            get => Node.GetAttribute("onsubmit", null);
        }

        /// <summary>
        /// The Html Node that pertains to this form.
        /// </summary>
        internal readonly IHtmlNode Node;

        /// <summary>
        /// The Page that this form came from.
        /// </summary>
        public readonly WebPage SourcePage;

        /// <summary>
        /// The Internal List instance.
        /// </summary>
        private readonly List<HtmlFormControl> _List = new List<HtmlFormControl>();

        #region IReadOnlyList Methods

        /// <summary>
        /// Returns an enumerator that iterates over the <see cref="HtmlForm"/>.
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<HtmlFormControl> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates over the <see cref="HtmlForm"/>.
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        /// <summary>
        /// Gets the number elements contained in the <see cref="HtmlForm"/>.
        /// </summary>
        public int Count => _List.Count;

        public HtmlFormControl this[int index] => _List[index];

        #endregion IReadOnlyList Methods
    }
}