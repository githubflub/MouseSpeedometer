using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseSpeedometer
{
    class MouseDataPacket
    {
        private int lastx;
        private int lasty;
        private double timestamp; 

        public MouseDataPacket(int lastx, int lasty, double timestamp)
        {
            this.lastx = lastx;
            this.lasty = lasty;
            this.timestamp = timestamp; 
        }

        public int getLastX() { return this.lastx; }
        public int getLastY() { return this.lasty; }
    }
}
