using HtmlAgilityPack;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mechanize.Forms
{
    /// <summary>
    /// Provides a Dictionary to get all forms, although not all Forms are available this way, such as ones with No Name, or duplicates. <para/>
    /// To access all available forms, with duplicates and nameless ones, use <see cref="AllForms"/>.
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

        /// <summary>
        /// Provides all forms, whether they have duplicate names, or no name at all.
        /// </summary>
        public IReadOnlyList<HtmlForm> AllForms => _AllForms;

        private List<HtmlForm> _AllForms;
        private Dictionary<string, HtmlForm> _FormDictionary;
        private readonly HtmlDocument Document;

        /// <summary>
        /// The Web Page that this <see cref="FormCollection"/> belongs to.
        /// </summary>
        public readonly WebPage SourcePage;

        #region IReadOnlyDictionary Overrides

        public HtmlForm this[string key] => _FormDictionary[key];
        public IEnumerable<string> Keys => _FormDictionary.Keys;
        public IEnumerable<HtmlForm> Values => _FormDictionary.Values;
        public int Count => _FormDictionary.Count;

        public bool ContainsKey(string key)
        {
            return _FormDictionary.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<string, HtmlForm>> GetEnumerator()
        {
            return _FormDictionary.GetEnumerator();
        }

        public bool TryGetValue(string key, out HtmlForm value)
        {
            return _FormDictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _FormDictionary.GetEnumerator();
        }

        #endregion IReadOnlyDictionary Overrides
    }
}