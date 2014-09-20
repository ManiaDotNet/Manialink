using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaNet.Manialink.Elements
{
    class ManialinkGraph : ManialinkElement
    {
        public ManialinkGraph()
            : base (ManialinkElement.ElementType.Graph)
        {
            this.validFields.Add("valign");
            this.validFields.Add("halign");
        }
    }
}
