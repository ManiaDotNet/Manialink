namespace ManiaNet.Manialink.Elements
{
    internal class ManialinkEntry : ManialinkLabel
    {
        public ManialinkEntry(ManialinkElement.ElementType type = ManialinkElement.ElementType.Entry)
            : base(type)
        {
            this.validFields.Remove("text");
            this.validFields.Remove("maxline");
            this.validFields.Remove("textprefix");
            this.validFields.Remove("textemboss");
            this.validFields.Add("default");
            this.validFields.Add("name");
        }

        /// <summary>
        /// The default text of the input.
        /// </summary>
        public string Text
        {
            get { return this.Get("default"); }
            set { this.Set("default", value); }
        }
    }
}