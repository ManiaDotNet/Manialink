using System.Collections.Generic;

namespace ManiaNet.Manialink.Elements
{
    internal class ManialinkInclude : ManialinkElement
    {
        public ManialinkInclude()
            : base(ManialinkElement.ElementType.Include)
        {
            this.validFields = new List<string>();
            this.validFields.Add("url");
        }

        /// <summary>
        /// The URL for the file to include.
        /// </summary>
        public string Url
        {
            get { return this.Get("url"); }
            set { this.Set("url", value); }
        }
    }
}