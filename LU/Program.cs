using System;
using System.Runtime.InteropServices.ComTypes;

namespace LU
{
    class Program
    {
        static void fillExample(double[,] A,double[,] L, double[,] U)
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

            for (int i = 0; i < 4; i++)
            {
                L[i, i] = 1;
            }

            L[0, 1] = 0;
            L[0, 2] = 0;
            L[0, 3] = 0;
            L[1, 2] = 0;
            L[1, 3] = 0;
            L[2, 3] = 0;

            U[1, 0] = 0;
            U[2, 0] = 0;
            U[2, 1] = 0;
            U[3, 0] = 0;
            U[3, 1] = 0;
            U[3, 2] = 0;

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
        static void PrintMatrixLU(double[,] A)
        {
            for (int i = 0; i < 4; i++)
            {
                string tmp = "";
                for (int j = 0; j < 4; j++)
                {
                    tmp += $"{A[i, j]}   ";
                }

                Console.WriteLine(tmp);
            }
            Console.WriteLine();
        }
        static void PrintMatrix(double[] x)
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"{x[i]}  ");
            }
            Console.WriteLine();
        }
        static void PrintMatrix(int[] x)
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"{x[i]}  ");
            }

            Console.WriteLine();
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
        static void LU(double[,] A, double[,] L, double[,] U, int size)
        {
            double sum = 0;
            U[0, 0] = A[0, 0];
            U[0, 1] = A[0, 1];
            U[0, 2] = A[0, 2];
            U[0, 3] = A[0, 3];
            L[1, 0] = A[1, 0] / U[0, 0];
            L[2, 0] = A[2, 0] / U[0, 0];
            L[3, 0] = A[3, 0] / U[0, 0];

            for (int i = 0; i < size; i++)
            {

                for (int j =0; j < size; j++)
                {
                    if (i <= j)
                    {
                        sum = 0;
                        for (int k = 0; k < i ; k++)
                            sum += L[i, k] * U[k, j];
                        U[i, j] = A[i, j] - sum;
                    }
                    if (i > j)
                    {
                        sum = 0;
                        for (int k = 0; k < j ; k++)
                            sum += L[i, k] * U[k, j];
                        L[i, j] = (A[i, j] - sum) / U[j, j];
                    }
                }
            }

            L[0, 4] = A[0, 4];
            L[1, 4] = A[1, 4];
            L[2, 4] = A[2, 4];
            L[3, 4] = A[3,4];

            double[] x = gauss(L, 4);
            U[0, 4] = x[0];
            U[1, 4] = x[1];
            U[2, 4] = x[2];
            U[3, 4] = x[3];
            x = gauss(U, 4);
            PrintMatrix(x);
        }


        static void Main(string[] args)
        {
            double[,] A = new double[4, 5];
            double[,] L = new double[4,5];
            double[,] U = new double[4, 5];
            double[] b = new double[4];
            double[] x;
            fillExample(A, L, U);
            LU(A,L,U,4);
        }
    }
}
