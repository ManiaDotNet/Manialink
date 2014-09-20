using System.Collections.Generic;

namespace ManiaNet.Manialink.Elements
{
    internal class ManialinkTimeout : ManialinkElement
    {
        /// <summary>
        /// Seconds to retain cached Manialink.
        /// </summary>
        public string Timeout { get; set; }

        public ManialinkTimeout(int timeout = 0)
            : base(ManialinkElement.ElementType.Timeout)
        {
            this.Timeout = timeout.ToString();
            this.validFields = new List<string>();
        }

        public override string ToString()
        {
            return @"<timeout>" + Timeout + "</timeout>";
        }
    }
}