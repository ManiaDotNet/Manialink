using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ManiaNet.Manialink.Elements
{
    class ManialinkDico : ManialinkElement
    {
        private Dictionary<Language, Dictionary<string, string>> dict = new Dictionary<Language, Dictionary<string, string>>();

        public enum Language { Czech, Danish, German, English, Spanish, French, Hungarian, Italian, Japanese, Korean, Norwegian,
            Netherlandic, Polish, Portuguese, BrazilianPortuguese, Romanian, Russian, Slowakian, Turkish, Chinese }
        private string[] langNames = { "cz", "da", "de", "en", "es", "fr", "hu", "it", "jp", "kr", "nb", "nl", "pl", "pt", "pt_BR",
                                     "ro", "ru", "sk", "tr", "zh" };

        public ManialinkDico()
            : base(ManialinkElement.ElementType.Dico)
        {
            this.Closed = false;
            this.validFields = new List<string>();
        }

        /// <summary>
        /// Creates a dico element from a xml node.
        /// </summary>
        /// <param name="node">The &lt;dico&gt;-node.</param>
        public ManialinkDico(XmlNode node)
            : base(ManialinkElement.ElementType.Dico)
        {
            this.Closed = false;
            this.validFields = new List<string>();
            if (node.Name.ToLower() == "dico")
            {
                foreach (XmlNode lang in node.ChildNodes)
                {
                    if (lang.Name.ToLower() == "language")
                    {
                        try
                        {
                            string name = lang.Attributes["id"].Value.ToLower();
                            if (langNames.Contains(name))
                            {
                                Language l = (Language)Array.IndexOf(langNames, name);
                                foreach (XmlNode ident in lang.ChildNodes)
                                {
                                    this.Add(l, ident.Name, ident.Value);
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        /// <summary>
        /// Add a translation to the dict for the specified language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="ident">The identifier.</param>
        /// <param name="translation">The translation.</param>
        /// <returns>This.</returns>
        public ManialinkDico Add(ManialinkDico.Language language, string ident, string translation)
        {
            if (!dict.ContainsKey(language))
                dict.Add(language, new Dictionary<string, string>());
            try
            {
                dict[language].Add(ident, translation);
            }
            catch (ArgumentException)
            {
                dict[language][ident] = translation;
            }
            return this;
        }
        /// <summary>
        /// Add multiple translations to the dict for the specified language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="values">The translations.</param>
        /// <returns>This.</returns>
        public ManialinkDico Add(ManialinkDico.Language language, Dictionary<string, string> values)
        {
            if (!dict.ContainsKey(language))
                dict.Add(language, values);
            else
            {
                foreach (var v in values)
                {
                    this.Add(language, v.Key, v.Value);
                }
            }
            return this;                
        }
        /// <summary>
        /// Removes the given identifier from the dictionary for the specified language.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="ident">The identifier.</param>
        /// <returns>This.</returns>
        public ManialinkDico Remove(ManialinkDico.Language language, string ident)
        {
            if (dict.ContainsKey(language))
            {
                dict[language].Remove(ident);
            }
            return this;
        }
        /// <summary>
        /// Removes the given identifier from the dictionary for the all languages.
        /// </summary>
        /// <param name="ident">The identifier.</param>
        /// <returns>This.</returns>
        public ManialinkDico Remove(string ident)
        {
            foreach (var lang in dict.Keys)
            {
                dict[lang].Remove(ident);
            }
            return this;
        }
        /// <summary>
        /// Retrieves the dictionary for the specified language.
        /// </summary>
        /// <param name="language">The language</param>
        /// <returns>A dictionary with all values for the given language.</returns>
        public Dictionary<string, string> Get(ManialinkDico.Language language)
        {
            try
            {
                return dict[language];
            }
            catch
            {
                return null;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("<dico>");
            foreach (var lang in dict.Keys)
            {
                result.Append(@"<language id=""" + this.langNames[(int)lang] + @""">");
                foreach (var translation in dict[lang])
                {
                    result.Append("<" + translation.Key + ">" + translation.Value + "</" + translation.Key + ">");
                }
                result.Append("</language>");
            }
            result.Append("</dico>");
            return result.ToString();
        }
    }
}
