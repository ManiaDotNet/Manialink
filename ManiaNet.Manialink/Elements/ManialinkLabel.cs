namespace ManiaNet.Manialink.Elements
{
    public class ManialinkLabel : ManialinkElement
    {
        public ManialinkLabel(ManialinkElement.ElementType type = ManialinkElement.ElementType.Label)
            : base(type)
        {
            this.validFields.Add("valign");
            this.validFields.Add("halign");
            this.validFields.Add("url");
            this.validFields.Add("urlid");
            this.validFields.Add("manialink");
            this.validFields.Add("manialinkid");
            this.validFields.Add("action");
            this.validFields.Add("opacity");
            this.validFields.Add("rotation");
            this.validFields.Add("style");
            this.validFields.Add("textcolor");
            this.validFields.Add("textcolorfocus");
            this.validFields.Add("focusareacolor1");
            this.validFields.Add("focusareacolor2");
            this.validFields.Add("textsize");
            this.validFields.Add("textprefix");
            this.validFields.Add("textemboss");
            this.validFields.Add("textsize");
            this.validFields.Add("autonewline");
            this.validFields.Add("text");
            this.validFields.Add("textid");
            this.validFields.Add("maxline");
        }

        /// <summary>
        /// The label's text.
        /// </summary>
        public string Text
        {
            get { return this.Get("text"); }
            set { this.Set("text", value); }
        }

        /// <summary>
        /// Set the focusareacolors.
        /// </summary>
        /// <param name="color1">The standard background color.</param>
        /// <param name="color2">The mouseover background color.</param>
        /// <returns>This.</returns>
        public ManialinkLabel FocusareaColor(string color1, string color2 = null)
        {
            if (color2 == null)
                return (ManialinkLabel)this.Set("focusareacolor1", color1).Set("focusareacolor2", color1);
            return (ManialinkLabel)this.Set("focusareacolor1", color1).Set("focusareacolor2", color2);
        }
    }
}