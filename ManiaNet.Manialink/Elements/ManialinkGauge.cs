using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaNet.Manialink.Elements
{
    class ManialinkGauge : ManialinkElement
    {
        public ManialinkGauge()
            : base(ManialinkElement.ElementType.Gauge)
        {
            this.validFields.Add("halign");
            this.validFields.Add("valign");
            this.validFields.Add("style");
            this.validFields.Add("ratio");
            this.validFields.Add("rotation");
            this.validFields.Add("centered");
            this.validFields.Add("drawbg");
            this.validFields.Add("drawblockbg");
            this.validFields.Add("clan");
            this.validFields.Add("grading");
        }
    }
}
