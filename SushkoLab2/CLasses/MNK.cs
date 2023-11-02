using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SushkoLab2.CLasses
{
    public class MNK
    {
        public MNK(double[] x_array, double[] y_array, int order)
        {
            x_k = x_array;
            y_k = y_array;
            this.order = order;
            Generate();
        }

        public int order = 2;
        public const int pointCount = 10;
        public double[] y_k = new double[pointCount];
        public double[] x_k = new double[pointCount];

        public class TMatrix
        {
            public double[,] c;
            public double[] x;
            public double[] d;

            public TMatrix(int size)
            {
                c = new double[size, size];
                x = new double[size];
                d = new double[size];
            }
        }

        private TMatrix matrix;

        public class TGauss
        {
            public bool calcError;
            public int maxOrder = pointCount;
            TMatrix m;

            void SwitchRows(int n)
            {
                double tempD;
                int i, j;
                for (i = n; i <= maxOrder - 2; i++)
                {
                    for (j = 0; j <= maxOrder - 1; j++)
                    {
                        tempD = m.c[i, j];
                        m.c[i, j] = m.c[i + 1, j];
                        m.c[i + 1, j] = tempD;
                    }
                    tempD = m.d[i];
                    m.d[i] = m.d[i + 1];
                    m.d[i + 1] = tempD;
                }
            }

            public TGauss(int size, TMatrix mi)
            {
                maxOrder = size;
                m = mi;
            }

            /* build R-diagonal Matrix*/
            public bool Eliminate()
            {
                int i, k, l;
                if (Math.Abs(m.c[0, 0]) < 1e-8)
                    SwitchRows(0);
                calcError = false;
                for (k = 0; k <= maxOrder - 2; k++)
                {
                    for (i = k; i <= maxOrder - 2; i++)
                    {
                        if (Math.Abs(m.c[i + 1, i]) < 1e-8)
                        {
                            SwitchRows(i + 1);
                        }
                        if (m.c[i + 1, k] != 0.0)
                        {
                            for (l = k + 1; l <= maxOrder - 1; l++)
                            {
                                if (!calcError)
                                {
                                    m.c[i + 1, l] = m.c[i + 1, l] * m.c[k, k] - m.c[k, l] * m.c[i + 1, k];
                                    if (m.c[i + 1, l] > 10E260)
                                    {
                                        m.c[i + 1, k] = 0;  // range overflow 
                                        calcError = true;
                                    }
                                }
                            }
                            m.d[i + 1] = m.d[i + 1] * m.c[k, k] - m.d[k] * m.c[i + 1, k];
                            m.c[i + 1, k] = 0;
                        }
                    }
                }
                return !calcError;
            }

            /*solve equation with R-diagonal Matrix*/
            public void Solve()
            {
                int k, l;
                for (k = maxOrder - 1; k >= 0; k--)
                {
                    for (l = maxOrder - 1; l >= k; l--)
                    {
                        m.d[k] = m.d[k] - m.x[l] * m.c[k, l];
                    }
                    if (m.c[k, k] != 0)
                        m.x[k] = m.d[k] / m.c[k, k];
                    else
                        m.x[k] = 0;
                }
            }
        }

        public void Generate()
        {
            int i, j, k;
            if (order > pointCount)
                order = pointCount;
            if (order <= 1)
                order = 1;
            TMatrix a = new TMatrix(order);
            TMatrix m = new TMatrix(pointCount);
            TGauss gauss = new TGauss(order, a);
            // fill matrix
            for (i = 0; i < pointCount; i++)
            {
                m.d[i] = y_k[i];
                for (k = 0; k < order; k++)
                {
                    m.c[i, k] = Math.Pow(x_k[i], k);
                }
            }

            //calc a * transposed(a) and y * transposed(a)
            for (i = 0; i < order; i++)
            {
                a.d[i] = 0.0;
                for (k = 0; k < pointCount; k++)
                    a.d[i] = a.d[i] + m.c[k, i] * m.d[k];

                for (j = 0; j < order; j++)
                {
                    a.c[j, i] = 0.0;
                    for (k = 0; k < pointCount; k++)
                    {
                        a.c[j, i] = a.c[j, i] + m.c[k, j] * m.c[k, i];
                    }
                }
            }
            /*solve Matrix equation by Gauss*/
            if (gauss.Eliminate())
            {
                gauss.Solve();
                matrix = a;
            }
            else
                MessageBox.Show("Matrix calculattion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public double Evaluate(double x)
        {
            double y = 0;
            for (int k = 0; k < order; k++)
            {
                y += matrix.x[k] * Math.Pow(x, (double)(k));
            }
            return y;
        }
    }
}
