using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaNet.Manialink.Elements
{
    class ManialinkVideo : ManialinkElement
    {
        public ManialinkVideo()
            : base(ManialinkElement.ElementType.Video)
        {
            this.validFields.Add("halign");
            this.validFields.Add("valign");
            this.validFields.Add("data");
            this.validFields.Add("dataid");
            this.validFields.Add("play");
            this.validFields.Add("looping");
            this.validFields.Add("music");
            this.validFields.Add("volume");
        }
    }
}
