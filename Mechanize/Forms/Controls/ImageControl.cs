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

using HtmlAgilityPack;

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// An Image control that acts like a <see cref="SubmitControl"/>. <para/>
    /// Covers: INPUT/IMAGE <para/>
    /// Can handle Coordinates when Submitting content. (Not Implemented yet).
    /// </summary>
    public class ImageControl : SubmitControl
    {
        internal ImageControl(HtmlForm Form, HtmlNode Node) : base(Form, Node)
        {
        }
    }
}