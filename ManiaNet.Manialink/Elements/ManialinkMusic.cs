using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaNet.Manialink.Elements
{
    class ManialinkMusic: ManialinkElement
    {
        public ManialinkMusic(string data = null)
            :base(ManialinkElement.ElementType.Music)
        {
            this.validFields = new List<string>();
            this.validFields.Add("data");
            this.Set("data", data);
        }

        /// <summary>
        /// The url to the music file.
        /// </summary>
        public string Url
        {
            get { return this.Get("data"); }
            set { this.Set("data", value); }
        }
    }
}
