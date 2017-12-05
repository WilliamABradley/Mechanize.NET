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

namespace Mechanize.Forms.Controls.Options
{
    /// <summary>
    /// An option pertaining to a <see cref="SelectControl"/>.
    /// </summary>
    public class SelectOption : ListOption
    {
        internal SelectOption(ListControl Parent, IHtmlNode Node) : base(Parent, Node)
        {
        }

        /// <summary>
        /// The Value used for creating the Submission form, an amalgamation of Value, Label, and Name, whatever isn't null.
        /// </summary>
        public override string TransmitValue =>
            !string.IsNullOrWhiteSpace(Value) ? Value :
            !string.IsNullOrWhiteSpace(Label) ? Label :
            Name;

        /// <summary>
        /// Determines if the current Option is selected, if Setting to true, it will deselect all other options, as a <see cref="SelectControl"/> only supports one Selection.
        /// </summary>
        public override bool Selected
        {
            get => base.Selected;
            set
            {
                if (value == true)
                {
                    foreach (var option in Parent.Options)
                    {
                        if (option != this)
                        {
                            option.Selected = false;
                        }
                    }
                }
                base.Selected = value;
            }
        }
    }
}