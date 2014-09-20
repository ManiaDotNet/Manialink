using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaNet.Manialink.Elements
{
    class ManialinkAudio : ManialinkElement
    {
        public ManialinkAudio()
            : base (ManialinkElement.ElementType.Audio)
        {
            this.validFields.Add("valign");
            this.validFields.Add("halign");
            this.validFields.Add("data");
            this.validFields.Add("dataid");
            this.validFields.Add("play");
            this.validFields.Add("looping");
            this.validFields.Add("music");
            this.validFields.Add("volume");
        }
    }
}
