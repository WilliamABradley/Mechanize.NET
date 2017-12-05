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

using Mechanize.Html;
using Mechanize.Requests;
using System.Collections.Generic;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// File Control for adding File Data to a Form. <para/>
    /// Use <see cref="SelectFile(byte[], string)"/> to add a File to the Control.
    /// </summary>
    public class FileControl : HtmlFormControl
    {
        internal FileControl(HtmlForm Form, IHtmlNode Node) : base(Form, Node)
        {
        }

        /// <summary>
        /// Selects a File to add to the Form.
        /// </summary>
        /// <param name="FileData">Byte Array of File Data.</param>
        /// <param name="FileName">Name of the File to Add.</param>
        public void SelectFile(byte[] FileData, string FileName = null)
        {
            this.FileData = FileData;
            this.FileName = FileName;
        }

        /// <summary>
        /// Gets the Request Info, if <see cref="HasFile"/>.
        /// </summary>
        /// <returns>Request Info.</returns>
        internal override List<IRequestInfo> GetRequestInfo()
        {
            if (HasFile) return new List<IRequestInfo> { new FileRequestInfo(FileData, FileName) };
            else return null;
        }

        /// <summary>
        /// Determines if there is a file in the control to add to the Form Submission.
        /// </summary>
        public bool HasFile => FileData != null;

        /// <summary>
        /// Name of the File to add to the Form Submission.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Byte Array of File Data, to add to the Form Submission.
        /// </summary>
        public byte[] FileData { get; private set; }
    }
}