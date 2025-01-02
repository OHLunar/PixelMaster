using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelMaster
{
    internal class CustomToolstrip : ToolStripSystemRenderer
    {
        // Custom renderer class to remove toolstrip border

        public CustomToolstrip() { }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) // Empty override for removing toolstrip border
        {
        }
    }
}
