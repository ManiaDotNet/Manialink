using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ManiaNet.Manialink.Elements
{
    public class IllegalElementException : Exception, ISerializable
    {
        public IllegalElementException()
            : base()
        { }

        public IllegalElementException(string message)
            : base(message)
        { }

        public IllegalElementException(string message, Exception inner)
            : base(message, inner)
        { }

        protected IllegalElementException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }

    public class ManialinkElement
    {
        public enum ElementType { Label, Quad, Frame, Gauge, Frame3D, Audio, Music, Video, Entry, FileEntry, Include, Script, Graph, Format, Timeout, Dico };

        protected List<string> validFields = new List<string>();
        protected Dictionary<string, string> attributes = new Dictionary<string, string>();

        /// <summary>
        /// The type the ManialinkElement is.
        /// </summary>
        public ElementType Type { get; private set; }

        /// <summary>
        /// Is the tag closed immediately like &lt;tag /&gt; or with an end-tag like &lt;tag&gt;&lt;/tag&gt;
        /// </summary>
        public bool Closed { get; protected set; }

        protected List<ManialinkElement> children = new List<ManialinkElement>();

        protected ManialinkElement(ElementType type)
        {
            this.Type = type;
            this.Closed = true;
            validFields.Add("id");
            validFields.Add("sizen");
            validFields.Add("scale");
            validFields.Add("posn");
            validFields.Add("hidden");
            validFields.Add("class");
            validFields.Add("scriptevents");
        }

        /// <summary>
        /// Sets an attribute for the element.
        /// </summary>
        /// <param name="attribute">Attributes name.</param>
        /// <param name="value">Attributes value. Leave empty to unset the attribute.</param>
        /// <returns>This.</returns>
        public ManialinkElement Set(string attribute, string value = null)
        {
            attribute = attribute.ToLower();
            if (String.IsNullOrEmpty(attribute))
            {
                return this;
            }
            if (String.IsNullOrEmpty(value))
            {
                this.attributes.Remove(attribute);
                return this;
            }
            if (!this.validFields.Contains(attribute))
            {
                throw new ArgumentException(String.Format("{0} is not valid for Elements of type {1}.", attribute, this.Type.ToString()));
            }
            try
            {
                this.attributes.Add(attribute, value);
            }
            catch (ArgumentException)
            {
                this.attributes[attribute] = value;
            }
            return this;
        }

        /// <summary>
        /// Returns the value for the requested attribute if available.
        /// </summary>
        /// <param name="attribute">The requested attribute name.</param>
        /// <returns></returns>
        public string Get(string attribute)
        {
            attribute = attribute.ToLower();
            if (this.attributes.ContainsKey(attribute))
            {
                return this.attributes[attribute];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns all set attributes.
        /// </summary>
        /// <returns>List of set attributes.</returns>
        public IEnumerable<string> Attributes()
        {
            return this.attributes.Keys.ToList();
        }

        /// <summary>
        /// Returns the XMl-representation of this element with all subelements.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            string tagname = this.Type.ToString().ToLower();
            StringBuilder innerCode = new StringBuilder();
            this.children.All(e => { innerCode.Append(e.ToString()); return true; });
            StringBuilder result = new StringBuilder();
            result.Append(@"<" + tagname);
            this.attributes.All(t => { result.Append(String.Format(@" {0}=""{1}""", t.Key, t.Value)); return true; });
            result.Append(Closed ? @" />" : ">" + innerCode.ToString() + "</" + tagname + ">");
            return result.ToString();
        }

        /// <summary>
        /// Merges all attributes of a second ManialinkElement into this one.
        /// Existing attributes will be overwritten!
        /// </summary>
        /// <param name="element2">The element to merge.</param>
        /// <returns>This element with extended attributes.</returns>
        public ManialinkElement Merge(ManialinkElement element2)
        {
            if (this.Type == element2.Type)
            {
                foreach (string attr in element2.Attributes())
                {
                    this.Set(attr, element2.Get(attr));
                }
                return this;
            }
            else
            {
                throw new ArgumentException(String.Format("Given Element is of type {0}, expecting {1}", element2.Type.ToString(), this.Type.ToString()));
            }
        }

        /// <summary>
        /// Splits the classes string and returns the parts.
        /// </summary>
        /// <returns>The seperated classes.</returns>
        private List<string> getClasses()
        {
            string x = this.Get("class");
            if (x != null)
            {
                return x.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            }
            return new List<string>();
        }

        /// <summary>
        /// Add a class to the element.
        /// </summary>
        /// <param name="name">The class name.</param>
        /// <returns>This.</returns>
        public ManialinkElement AddClass(string name)
        {
            List<string> classes = this.getClasses();
            classes.Add(name);
            this.Set("class", String.Join(" ", classes));
            return this;
        }

        /// <summary>
        /// Remove a class from the element.
        /// </summary>
        /// <param name="name">The class name.</param>
        /// <returns>This.</returns>
        public ManialinkElement RemoveClass(string name)
        {
            List<string> classes = this.getClasses();
            classes.Remove(name);
            this.Set("class", String.Join(" ", classes));
            return this;
        }

        /// <summary>
        /// Checks whether the element has the specified class.
        /// </summary>
        /// <param name="name">The class name.</param>
        /// <returns>True if the element has the specified class.</returns>
        public bool HasClass(string name)
        {
            return this.getClasses().Contains(name);
        }
    }
}