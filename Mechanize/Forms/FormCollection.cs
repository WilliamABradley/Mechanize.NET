using HtmlAgilityPack;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mechanize.Forms
{
    /// <summary>
    /// Provides a Dictionary to get all forms, although not all Forms are available this way, such as ones with No Name, or duplicates. <para/>
    /// To access all available forms, with duplicates and nameless ones, use <see cref="Values"/>.
    /// </summary>
    public class FormCollection : IReadOnlyDictionary<string, HtmlForm>
    {
        internal FormCollection(WebPage SourcePage, HtmlDocument Document)
        {
            this.SourcePage = SourcePage;
            this.Document = Document;

            var forms = Document.DocumentNode.Descendants()?
                .Where(item => item.Name == "form");

            _AllForms = forms
                .Select(form => new HtmlForm(SourcePage, form))
                .ToList();

            _FormDictionary = _AllForms
                .Distinct()
                .Where(item => !string.IsNullOrWhiteSpace(item.Name))
                .ToDictionary(item => item.Name, item => item);
        }

        private List<HtmlForm> _AllForms;
        private Dictionary<string, HtmlForm> _FormDictionary;
        private readonly HtmlDocument Document;

        /// <summary>
        /// The Web Page that this <see cref="FormCollection"/> belongs to.
        /// </summary>
        public readonly WebPage SourcePage;

        #region IReadOnlyDictionary Overrides

        public HtmlForm this[string key] => _FormDictionary[key];

        /// <summary>
        /// Gets a collection containing all of the Keys in the <see cref="FormCollection"/>.
        /// </summary>
        public IEnumerable<string> Keys => _FormDictionary.Keys;

        /// <summary>
        /// Gets all HtmlForms in the <see cref="FormCollection"/>, including Forms without a Name, or Duplicate Names.
        /// </summary>
        public IEnumerable<HtmlForm> Values => _AllForms;

        /// <summary>
        /// Gets the number of Name/Form pairs contained in the <see cref="FormCollection"/>.
        /// </summary>
        public int Count => _FormDictionary.Count;

        /// <summary>
        /// Determines whether the <see cref="FormCollection"/> contains the Specified Key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="FormCollection"/></param>
        /// <returns>True if the <see cref="FormCollection"/> contains an element with the specified key; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Key is null.</exception>
        public bool ContainsKey(string key)
        {
            return _FormDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Returns an iterator that iterates through the <see cref="FormCollection"/>.
        /// </summary>
        /// <returns> A <see cref="Dictionary.Enumerator"/> structure for the <see cref="FormCollection"/>.</returns>
        public IEnumerator<KeyValuePair<string, HtmlForm>> GetEnumerator()
        {
            return _FormDictionary.GetEnumerator();
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>true if the <see cref="FormCollection"/> contains an element with the specified key; otherwise, false.</returns>
        public bool TryGetValue(string key, out HtmlForm value)
        {
            return _FormDictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Returns an iterator that iterates through the <see cref="FormCollection"/>.
        /// </summary>
        /// <returns> A <see cref="Dictionary.Enumerator"/> structure for the <see cref="FormCollection"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _FormDictionary.GetEnumerator();
        }

        #endregion IReadOnlyDictionary Overrides
    }
}