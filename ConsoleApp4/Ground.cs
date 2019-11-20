﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Ground
    {
        public double x1, y1, x2, y2;
        public double x, y, len, rot;

        public Ground(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;

            x = (x1 + x2) / 2;
            y = (y1 + y2) / 2;

            len = Helper.DistanceFromTwoPoints(x1, y1, x2, y2);
            rot = Math.Atan2((y2 - y1), (x2 - x1));
        }
    }
}
