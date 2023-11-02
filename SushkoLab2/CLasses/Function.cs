using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushkoLab2.CLasses
{
    public static class Function
    {
        const double k1 = 0.07;
        const double k2 = 7.0;
        private const double c0 = 0.05346;
        private const double c1 = 0;
        private const double c2 = -1.28618;
        private const double c3 = 0;
        public static double[] mas_x = { -1.6000 + k2, -1.2000 + k2, -0.8000 + k2, -0.4000 + k2, 0 + k2, 0.4000 + k2, 0.8000 + k2, 1.2000 + k2, 1.6000 + k2, 2.0000 + k2 };
        public static double[] mas_y1 = { -0.2000 + k1, 0.6000 + k1, 1.4000 + k1, 2.2000 + k1, 3.0000 + k1, 3.8000 + k1, 4.6000 + k1, 5.4000 + k1, 6.2000 + k1, 7.0000 + k1 };
        public static double[] mas_y2 = { 4.3200 + k1, 3.2800 + k1, 2.8800 + k1, 3.1200 + k1, 4.0000 + k1, 5.5200 + k1, 7.6800 + k1, 10.4800 + k1, 13.9200 + k1, 18.0000 + k1 };
        private static MNK mnk1 = new MNK(mas_x, mas_y1, 2);
        private static MNK mnk2 = new MNK(mas_x, mas_y2, 3);
        public static double Evaluate(double x)
        {
            double y = Math.Sqrt(x * x + 2) * Math.Cos(2.14 * x);
            return y;
        }
        public static double EvaluateMidQ(double x) // polinom Chebusheva
        {
            double y = c0 + c1 * x + c2 * (2 * x * x - 1) + c3 * (4 * x * x * x - 3 * x);
            return y;
        }
        public static double EvaluateMnk1(double x) // Mnk
        {
            return mnk1.Evaluate(x);
        }
        public static double EvaluateMnk2(double x)  //Mnk
        {
            return mnk2.Evaluate(x);
        }
    }
}
