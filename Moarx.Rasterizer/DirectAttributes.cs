using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moarx.Rasterizer {
    public class DirectAttributes {

        public DirectAttributes(DirectColor lineColor, bool isFilled, int lineThickness, DirectColor fillColor = new()) {
            IsFilled= isFilled;
            FillColor= fillColor;
            LineColor= lineColor;
            LineThickness= lineThickness;
        }

        public bool IsFilled;
        public DirectColor LineColor;
        public DirectColor FillColor;
        public int LineThickness;

    }
}
