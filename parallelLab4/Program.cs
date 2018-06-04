using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Diagnostics;
namespace parallelLab4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = new int[100000];
            Console.Write("p = ");
            int p = int.Parse(Console.ReadLine());
            int[][] y = new int[p][];
            int min, max;
            Random r = new Random();
            Thread[] th = new Thread[p];
            /* for (int i = 0; i < A.Length; ++i)
             {
                 A[i] = r.Next(1, A.Length);
                 //Console.Write(A[i] + " ");
             }
             */
            for (int i = 0; i < A.Length; ++i)
            {
                if (i < 9)
                    A[i] = 10;
                else
                    A[i] = r.Next(1000, 2000);
            }

            Console.WriteLine("\n");
            min = max = A[0];
            for (int i = 0; i < A.Length; ++i)
            {
                max = A[i] > max ? A[i] : max;
                min = A[i] < min ? A[i] : min;
            }
            int average = (max + min) / p;
            ArrayList el = new ArrayList();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < p; ++i)
            {
                int mini = min + i * average, maxi = min + (i + 1) * average;
                foreach (int x in A)
                    if (x >= mini && x < maxi)
                        el.Add(x);
                th[i] = new Thread(Sort);
                y[i] = new int[el.Count];
                el.CopyTo(y[i]);
                th[i].Start(y[i]);
                Console.WriteLine(y[i].Length);
                el.Clear();
            }
            foreach (Thread t in th)
                t.Join();
            int cnt = 0;
            while (cnt < A.Length)
            {
                for (int i = 0; i < p; ++i)
                    for (int j = 0; j < y[i].Length; ++j)
                    {
                        if (cnt >= A.Length) break;
                        A[cnt] = y[i][j];
                        cnt++;
                    }
            }
            sw.Stop();
            Console.WriteLine("Sorted:");
            /*foreach (int x in A)
                Console.Write(x + " ");*/
            Console.WriteLine();
            Console.WriteLine("Time = " + sw.ElapsedMilliseconds);
            Console.ReadLine();
        }

        static void Sort(object c)
        {
            int[] x = (int[])c;
            Array.Sort(x);
        }

    }
}
