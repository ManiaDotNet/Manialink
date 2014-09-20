using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaNet.Manialink.Elements
{
    class ManialinkFormat : ManialinkElement
    {
        public ManialinkFormat()
            : base(ManialinkElement.ElementType.Format)
        {
            this.validFields = new List<string>();
            this.validFields.Add("bgcolor");
            this.validFields.Add("textsize");
            this.validFields.Add("textcolor");
            this.validFields.Add("focusareacolor1");
            this.validFields.Add("focusareacolor2");
            this.validFields.Add("style");
        }
    }
}
