using HtmlAgilityPack;
using Mechanize.Requests;
using System.Collections.Generic;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// File Control for adding File Data to a Form. <para/>
    /// The Value of this Control is always null, use <see cref="SelectFile(byte[], string)"/> instead.
    /// </summary>
    public class FileControl : HtmlFormControl
    {
        internal FileControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
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