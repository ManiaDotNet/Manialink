namespace ManiaNet.Manialink.Elements
{
    public class ManialinkQuad : ManialinkElement
    {
        public ManialinkQuad()
            : base(ManialinkElement.ElementType.Quad)
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
            this.validFields.Add("image");
            this.validFields.Add("imageid");
            this.validFields.Add("imagefocus");
            this.validFields.Add("imagefocusid");
            this.validFields.Add("style");
            this.validFields.Add("substyle");
            this.validFields.Add("bgcolor");
            this.validFields.Add("bgcolorfocus");
            this.validFields.Add("colorize");
        }

        /// <summary>
        /// Set style and substyle or the image, if no substyle is given.
        /// </summary>
        /// <param name="style">The style or the imageurl.</param>
        /// <param name="substyle">The substyle.</param>
        /// <returns>This.</returns>
        public ManialinkQuad Style(string style, string substyle = null)
        {
            if (substyle == null)
                return (ManialinkQuad)this.Set("image", style);
            return (ManialinkQuad)this.Set("style", style).Set("substyle", substyle);
        }

        /// <summary>
        /// Set image and imagefocus.
        /// </summary>
        /// <param name="image">The image url.</param>
        /// <param name="imagefocus">The imagefocus url.</param>
        /// <returns>This.</returns>
        public ManialinkQuad Image(string image, string imagefocus = null)
        {
            if (imagefocus == null)
                return (ManialinkQuad)this.Set("image", image);
            return (ManialinkQuad)this.Set("imagefocus", imagefocus);
        }
    }
}