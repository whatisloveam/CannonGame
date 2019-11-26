using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Helper
    {
        public static double DistanceFromTwoPoints(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public static Tuple<double, double> CalculateDistances(int x1, int y1, int x2, int y2)
        {
            double p = 0.1 * DistanceFromTwoPoints(x1, x2, y1, y2);
            double a = (double)((-180 / Math.PI) * (Math.Atan2(y2 - y1, x2 - x1) - Math.PI));
            return new Tuple<double, double>(p, a);
        }
    }
}
