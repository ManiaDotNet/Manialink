using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManiaNet.Manialink.Elements
{
    class ManialinkScript : ManialinkElement
    {
        private StringBuilder code = new StringBuilder();

        public ManialinkScript()
            : base(ManialinkElement.ElementType.Script)
        {
            this.validFields = new List<string>();
            this.Closed = false;
        }

        public ManialinkScript AddCode(string code)
        {
            this.code.Append(code);
            return this;
        }

        public override string ToString()
        {
            return @"<script><!--" + this.code.ToString() + "--></script>";
        }
    }
}
