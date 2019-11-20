using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    abstract class Constants
    {
        public static int WindowWidth;
        public static int WindowHeight;
        public static Vector gravity = new Vector(0, 0.05);
        public static int GroundSegments = 40;
    }
}
