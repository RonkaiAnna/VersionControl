using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week08.Abstractions;

namespace week08.Entities
{
    public class Present : Toy
    {
        public SolidBrush RibbonColor { get; private set; }
        public SolidBrush BoxColor { get; private set; }
        public Present(Color color1, Color color2)
        {
            RibbonColor = new SolidBrush(color1);
            BoxColor = new SolidBrush(color2);
        }
        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(BoxColor, 0, 0, Width, Height);
            g.FillRectangle(RibbonColor, Width / 5 * 2, 0, Width / 5, Height);
            g.FillRectangle(RibbonColor, 0, Height / 5 * 2, Width, Height / 5);

        }
    }
}
