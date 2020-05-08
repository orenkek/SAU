using System;
using System.Security.Cryptography;

namespace GaussModified
{
    class Program
    {
        static void fill(double[,] A)
        {
            A[0, 0] = -83;
            A[0, 1] = 27;
            A[0, 2] = -13;
            A[0, 3] = -11;
            A[0, 4] = 142;
            A[1, 0] = 5;
            A[1, 1] = -68;
            A[1, 2] = 13;
            A[1, 3] = 24;
            A[1, 4] = 26;
            A[2, 0] = 9;
            A[2, 1] = 54;
            A[2, 2] = 127;
            A[2, 3] = 36;
            A[2, 4] = 23;
            A[3, 0] = 13;
            A[3, 1] = 27;
            A[3, 2] = 34;
            A[3, 3] = 156;
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

        static void swapOfColumn(double[,] A, int[] x, int i, int j, int size)
        {
            for (int k = i; k < size; k++)
            {
                double temp = A[k, i];
                A[k, i] = A[k,j];
                A[k,j] = temp;
            }

            int tmp = x[i];
            x[i] = x[j];
            x[j] = tmp;
        }
        static void swapOfRow(double[,] A, int i, int j, int size)
        {
            for (int k = i; k < size+1; k++)
            {
                double temp = A[j,k];
                A[j, k] = A[i, k];
                A[i, k] = temp;
            }
        }

        static void swap(double[,] A, int[] x, int current, int row, int column, int size)
        {
            for (int k = 0; k < size; k++)
            {
                double temp = A[k, current];
                A[k, current] = A[k, column];
                A[k, column] = temp;

            }
            int tmp = x[current];
            x[current] = x[column];
            x[column] = tmp;

            for (int k = 0; k < size + 1; k++)
            {
                double temp = A[current, k];
                A[current, k] = A[row, k];
                A[row, k] = temp;
            }
        }
        static double[] gaussRow(double[,] A, int size)
        {
            double maxInRow = Math.Abs(A[0, 0]), tmp;
            int row = 0;
            int[] x = new int[4];
            x[0] = 1;
            x[1] = 2;
            x[2] = 3;
            x[3] = 4;
            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    maxInRow = Math.Abs(A[i, i]);
                    if (Math.Abs(A[i, j]) >= maxInRow)
                    {
                        maxInRow = Math.Abs(A[i, j]);
                        row = j;
                    }
                }
                swapOfColumn(A, x, i, row, size);
                tmp = A[i, i];
                for (int j = size; j >= i; j--)
                    A[i, j] /= tmp;
                PrintMatrix(A);
                for (int j = i + 1; j < size; j++)
                {
                    tmp = A[j, i];
                    for (int k = size; k >= i; k--)
                        A[j, k] -= tmp * A[i, k];
                }
                PrintMatrix(A);
            }
            double[] xx = new double[size];
            double[] X = new double[size];
            for (int i = size - 1; i >= 0; i--)
            {
                xx[i] = A[i, size] / A[i, i];
                for (int c = size - 1; c > i; c--)
                {
                    xx[i] = xx[i] - A[i, c] * xx[c] / A[i, i];
                }
            }

            for (int i = 0; i < size; i++)
            {
                X[x[i] - 1] = xx[i];
            }
            return X;

        }
        static double[] gaussColumn(double[,] A, int size)
        {
            double maxInColumn = Math.Abs(A[0, 0]), tmp;
            int column = 0;
            for (int i = 0; i < size-1; i++)
            {
                maxInColumn = A[i,i];
                for (int j = i; j < size; j++)
                {
                    if (Math.Abs(A[j,i]) > maxInColumn)
                    {
                        maxInColumn = Math.Abs(A[j,i]);
                        column = j;
                    }
                }
                swapOfRow(A, i, column, size);
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
            double[] xx = new double[size];
            double[] X = new double[size];
            for (int i = size - 1; i >= 0; i--)
            {
                xx[i] = A[i, size] / A[i, i];
                for (int c = size - 1; c > i; c--)
                {
                    xx[i] = xx[i] - A[i, c] * xx[c] / A[i, i];
                }
            }
            return xx;
        }



        static double[] gaussAll(double[,] A, int size)
        {
            double max = Math.Abs(A[0, 0]), tmp;
            int row=0, column=0;
            int[] x = new int[4];
            x[0] = 1;
            x[1] = 2;
            x[2] = 3;
            x[3] = 4;
            for (int i = 0; i < size-1; i++)
            {
                max = Math.Abs(A[i, i]);
                for(int k = i;k<size;k++)
                { for (int h = i; h < size; h++)
                {
                    if (Math.Abs(A[k, h]) > max)
                    {
                        max = Math.Abs(A[k,h]);
                        row = k;
                        column = h;
                    }
                }}
                swap(A,x,i,row,column,size);
                tmp = A[i, i];
                for (int j = size; j >= i; j--)
                    A[i, j] /= tmp;
                for (int j = i + 1; j < size; j++)
                {
                    tmp = A[j, i];
                    for (int k = size; k >= i; k--)
                        A[j, k] -= tmp * A[i, k];
                }
            }

            tmp = A[size - 1, size - 1];
            A[size - 1, size] /= tmp;
            A[size - 1, size - 1] /= tmp;
            PrintMatrix(x);
            double[] xx = new double[size];
            double[] X = new double[size];
            for (int i = size - 1; i >= 0; i--)
            {
                xx[i] = A[i, size] / A[i, i];
                for (int c = size - 1; c > i; c--)
                {
                    xx[i] = xx[i] - A[i, c] * xx[c] / A[i, i];
                }
            }

            for (int i = 0; i < size; i++)
            {
                X[x[i] - 1] = xx[i];
            }
            return X;





        }




        static void Main(string[] args)
        {
            double[,] A = new double[4, 5];
            double[] b = new double[4];
            double[] x;
            fillExample(A);
            PrintMatrix(A);
            //x = gaussRow(A, 4); // По строке
            //x = gaussColumn(A, 4); // По столбцу
            x = gaussAll(A,4);
            PrintMatrix(A);
            PrintMatrix(x);
        }
    }
}
