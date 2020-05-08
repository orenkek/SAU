using System;
using System.Globalization;

namespace SAU
{
    class Program
    {
        static void fill(double[,] A)
        {
            A[0, 0] = -83;
            A[0, 1] = 27;
            A[0,2] = -13;
            A[0,3] = -11;
            A[0, 4] = 142;
            A[1,0] = 5;
            A[1,1] = -68;
            A[1,2] = 13;
            A[1,3] = 24;
            A[1, 4] = 26;
            A[2,0] = 9;
            A[2,1] = 54;
            A[2,2] = 127;
            A[2,3] = 36;
            A[2, 4] = 23;
            A[3,0] = 13;
            A[3,1] = 27;
            A[3,2] = 34;
            A[3,3] = 156;
            A[3, 4] = 49;
        }

        static void fillExample(double[,] A)
        {
            A[0, 0] = 2;
            A[0, 1] = 1;
            A[0, 2] = -1;
            A[0, 3] = 1;
            A[0, 4] = 2.7;
            A[1, 0] = 0.4;
            A[1, 1] = 0.5;
            A[1, 2] = 4;
            A[1, 3] = -8.5;
            A[1, 4] = 21.9;
            A[2, 0] = 0.3;
            A[2, 1] = -1;
            A[2, 2] = 1;
            A[2, 3] = 5.2;
            A[2, 4] = -3.9;
            A[3, 0] = 1;
            A[3, 1] = 0.2;
            A[3, 2] = 2.5;
            A[3, 3] = -1;
            A[3, 4] = 9.9;
        }

        static double[] gauss(double[,] A, int size)
        {
            double tmp;
            for (int i = 0; i < size; i++)
            {
                tmp = A[i, i];
                for (int j = size; j >= i; j--)
                    A[i, j] /= tmp;
                for (int j = i + 1; j < size; j++)
                {
                    tmp = A[j, i];
                    for (int k = size; k >= i; k--)
                        A[j, k] -= tmp * A[i, k];
                }
                PrintMatrix(A);
            }
            double[] x = new double[size];

            for (int i = size - 1; i >= 0; i--)
            {
                x[i] = A[i, size] / A[i, i];
                for (int c = size - 1; c > i; c--)
                {
                    x[i] = x[i] - A[i, c] * x[c] / A[i, i];
                }
            }

            return x;
        }

        static void PrintMatrix(double[,] A)
        {
            for (int i = 0; i < 4; i++)
            {
                string tmp = "";
                for (int j = 0; j < 5; j++)
                {
                    tmp += $"{A[i, j]}   ";
                }

                Console.WriteLine(tmp);
            }

            Console.WriteLine();
        }

        static void PrintMatrix(double[] x)
        {
           for(int i = 0; i < 4;i++)
           {
               Console.Write($"{x[i]}  ");
           }

           Console.WriteLine();
        }
        static void Main(string[] args)
        {
            double[,] A = new double[4,5];
            double[] b = new double[4];
            double[] x;
            fillExample(A);
            x = gauss(A, 4);;
            PrintMatrix(x);
        }
    }
}
