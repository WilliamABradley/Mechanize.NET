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

namespace Mechanize.Forms.Controls
{
    /// <summary>
    /// Represents a Input control with the designated "type" attribute, or a Select or Button control.
    /// </summary>
    public enum FormControlType
    {
        /// <summary>
        /// The provided Type could not be found.
        /// </summary>
        Unknown,

        /// <summary>
        /// Represents a <see cref="SelectControl"/>.
        /// </summary>
        Select,

        /// <summary>
        /// Represents a <see cref="TextInputControl"/>.
        /// </summary>
        Email,

        /// <summary>
        /// Represents a <see cref="TextInputControl"/>.
        /// </summary>
        Text,

        /// <summary>
        /// Represents a <see cref="TextInputControl"/>. A Large Text Input control.
        /// </summary>
        TextArea,

        /// <summary>
        /// Represents a <see cref="SubmitControl"/>.
        /// </summary>
        Submit,

        /// <summary>
        /// Represents a <see cref="ButtonControl"/>. Ignore this control, when handling forms.
        /// </summary>
        Ignore,

        /// <summary>
        /// Represents a <see cref="TextInputControl"/>, handles a Password Field in the Form.
        /// </summary>
        Password,

        /// <summary>
        /// Represents a <see cref="TextInputControl"/>, stores data invisible to the User.
        /// </summary>
        Hidden,

        /// <summary>
        /// Represents a <see cref="RadioControl"/>.
        /// </summary>
        Radio,

        /// <summary>
        /// Represents a <see cref="CheckBoxControl"/>.
        /// </summary>
        Checkbox,

        /// <summary>
        /// Represents a <see cref="ImageControl"/>.
        /// </summary>
        Image,

        /// <summary>
        /// Represents a <see cref="FileControl"/>.
        /// </summary>
        File
    }
}