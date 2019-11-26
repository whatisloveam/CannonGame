using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CannonGame;

namespace ConsoleApp4
{
    abstract class Constants
    {
        public static int WindowWidth;
        public static int WindowHeight;
        public static Vector gravity = new Vector(0, 0.4);
        public static int GroundSegments = 40;
        public static int playerCount = 4;
        public static ColorARGB[] PlayersColors =
        {
            new ColorARGB(1,1,0,0),
            new ColorARGB(1,0,1,0),
            new ColorARGB(1,0,0,1),
            new ColorARGB(1,1,1,0),
        };
    }
}
