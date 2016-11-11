using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseSpeedometer
{
    class MouseEvent
    {
        public int lastx;
        public int lasty;

        public MouseEvent(int lastx, int lasty)
        {
            this.lastx = lastx;
            this.lasty = lasty;
        }
    }
}
