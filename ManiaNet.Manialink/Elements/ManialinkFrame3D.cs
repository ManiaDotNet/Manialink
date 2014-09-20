using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaNet.Manialink.Elements
{
    class ManialinkFrame3D : ManialinkElement
    {
        public ManialinkFrame3D()
            : base(ManialinkElement.ElementType.Frame3D)
        {
            this.validFields.Add("halign");
            this.validFields.Add("valign");
            this.validFields.Add("style3d");
        }
    }
}
