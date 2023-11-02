using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SushkoLab2.CLasses
{
    public static class Drawer
    {
        public static double xMin = -3;
        public static double xMax = 10;
        private static double yMin = -10;
        private static double yMax = 20;
        private static int i1, i2, j1, j2;
        private static Graphics gr;
        private static int xtoi(double x)
        {
            int ii;
            ii = i1 + (int)Math.Truncate((x - xMin) * ((i2 - i1) / (xMax - xMin)));
            return ii;
        }
        private static int ytoj(double y)
        {
            int jj;
            jj = j2 + (int)Math.Truncate((y - yMin) * (j1 - j2) / (yMax - yMin));
            return jj;
        }
        public static void DrawCoordinateSystem()
        {
            Pen pen_net = new Pen(Brushes.Gray, 2);
            pen_net.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            for (int p = (int)xMin; p <= (int)xMax; p++)
            {
                gr.DrawLine(pen_net, xtoi(p), ytoj(yMax), xtoi(p), ytoj(yMin));
            }
            for (int p = (int)yMin; p < (int)yMax; p++)
            {
                gr.DrawLine(pen_net, xtoi(xMin), ytoj(p), xtoi(xMax), ytoj(p));
            }
            Pen pen_os = new Pen(Brushes.Black, 2);
            pen_os.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            pen_os.StartCap = System.Drawing.Drawing2D.LineCap.Triangle;

            gr.DrawLine(pen_os, xtoi(xMin), ytoj(0), xtoi(xMax), ytoj(0));
            gr.DrawLine(pen_os, xtoi(0), ytoj(yMin), xtoi(0), ytoj(yMax));

            Font MyFont = new Font("Arial", 8, FontStyle.Regular);
            for (int p = 1; p <= xMax; p++)
                gr.DrawString(Convert.ToString(p), MyFont, Brushes.Black, new Point(xtoi(p - 0.2), ytoj(-0.05)));
            for (int p = -1; p >= xMin; p--)
                gr.DrawString(Convert.ToString(p), MyFont, Brushes.Black, new Point(xtoi(p - 0.2), ytoj(-0.06)));
            for (int p = 1; p <= yMax; p++)
                gr.DrawString(Convert.ToString(p), MyFont, Brushes.Black, new Point(xtoi(-0.5), ytoj(p + 0.1)));
            for (int p = -1; p >= yMin; p--)
                gr.DrawString(Convert.ToString(p), MyFont, Brushes.Black, new Point(xtoi(-0.5), ytoj(p + 0.1)));
        }
        public static void DrawPoint(double x, double y, Brush? brush = null)
        {
            Brush blackBrush = new SolidBrush(Color.Black);
            if (brush != null)
            {
                blackBrush = brush;
            }
            if (y >= yMin && y <= yMax)
            {
                gr.FillRectangle(blackBrush, xtoi(x) - 1, ytoj(y) - 1, 2, 2);
            }
        }
        public static void DrawPointBig(double x, double y, Brush? brush = null)
        {
            Brush blackBrush = new SolidBrush(Color.Black);
            if (brush != null)
            {
                blackBrush = brush;
            }
            gr.FillRectangle(blackBrush, xtoi(x) - 3, ytoj(y) - 3, 6, 6);
        }
        public static void DrawPointLarge(double x, double y, Brush? brush = null)
        {
            Brush blackBrush = new SolidBrush(Color.Black);
            if (brush != null)
            {
                blackBrush = brush;
            }
            gr.FillRectangle(blackBrush, xtoi(x) - 5, ytoj(y) - 5, 10, 10);
        }
        public static void DrawPointMed(double x, double y, Brush? brush = null)
        {
            Brush blackBrush = new SolidBrush(Color.Black);
            if (brush != null)
            {
                blackBrush = brush;
            }
            gr.FillRectangle(blackBrush, xtoi(x) - 2, ytoj(y) - 2, 4, 4);
        }

        public static void DrawFunction()
        {
            for (double pointX = xMin; pointX <= xMax; pointX += 0.001)
            {
                var pointY = Function.Evaluate(pointX);
                Brush brush = new SolidBrush(Color.DarkGray);
                DrawPointBig(pointX, pointY, brush);
            }
            for (double pointX = xMin; pointX <= xMax; pointX += 0.001)
            {
                var pointY = Function.EvaluateMidQ(pointX);
                Brush brush = new SolidBrush(Color.Blue);
                DrawPointMed(pointX, pointY, brush);
            }
    
            Brush brush1 = new SolidBrush(Color.Orange);

            DrawPoints(Function.mas_x, Function.mas_y1, brush1);

            for (double pointX = xMin; pointX <= xMax; pointX += 0.001)
            {
                var pointY = Function.EvaluateMnk1(pointX);
                Brush brush = new SolidBrush(Color.Green);
                DrawPoint(pointX, pointY, brush);
            }
            Brush brush2 = new SolidBrush(Color.Brown);
            DrawPoints(Function.mas_x, Function.mas_y2, brush2);
            for (double pointX = xMin; pointX <= xMax; pointX += 0.001)
            {
                var pointY = Function.EvaluateMnk2(pointX);
                Brush brush = new SolidBrush(Color.Red);
                DrawPoint(pointX, pointY, brush);
            }
        }
        public static void DrawPoints(double[] mas_x, double[] mas_y, Brush brush)
        {
            int x = 1;
            for(int i = 0; i < Math.Min(mas_x.Length, mas_y.Length); i++)
            {
                DrawPointLarge(mas_x[i], mas_y[i], brush);
            }
        }
        public static void CreateGraphics(PictureBox pictureBox)
        {
            gr = pictureBox.CreateGraphics();
            gr.Clear(Color.White);
            i1 = 0;
            j1 = 0;
            i2 = pictureBox.Width - 1;
            j2 = pictureBox.Height - 1;
        }
        public static void CLearAll()
        {
            gr.Clear(Color.White);
        }
    }
}
