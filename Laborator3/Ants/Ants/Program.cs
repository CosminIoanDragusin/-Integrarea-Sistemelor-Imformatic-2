using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Ants
{
    class Program
    {
        static double TotalDist(int[] route)
        {
            double d = 0.0;  // total distance between cities
            int n = route.Length;
            for (int i = 0; i < n - 1; ++i)
      {
                if (route[i] < route[i + 1])
          d += (route[i + 1] - route[i]) * 1.0;
        else
                    d += (route[i] - route[i + 1]) * 1.5;
            }
            return d;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\nBegin TSP simulated annealing C# ");

            int nCities = 34;
            Console.WriteLine("\n Numar de Orase = " + nCities);
            Console.WriteLine("\nOptimal solution is 0, 1, 2, . . " +
              (nCities - 1));
            Console.WriteLine("Optimal soln has total distance = " +
              (nCities - 1));
            Random rnd = new Random(2);
            int maxIter = 30000;
            double startTemperature = 12000.0;
            double alpha = 1.72;

            Console.WriteLine("\nSettings: ");
            Console.WriteLine("max_iter = " + maxIter);
            Console.WriteLine("start_temperature = " +
              startTemperature.ToString("F1"));
            Console.WriteLine("alpha = " + alpha);

            Console.WriteLine("\nStarting solve() ");
            int[] soln = Solve(nCities, rnd, maxIter,
              startTemperature, alpha);
            Console.WriteLine("Finished solve() ");

            Console.WriteLine("\nBest solution found: ");
            ShowArray(soln);
            double dist = TotalDist(soln);
            Console.WriteLine("\nTotal distance = " +
              dist.ToString("F1"));
 
            Console.ReadLine();

        }

        static double Error(int[] route)
        {
            int n = route.Length;
            double d = TotalDist(route);
            double minDist = n - 1;
            return d - minDist;
        }
        static int[] Adjacent(int[] route, Random rnd)
        {
            int n = route.Length;
            int[] result = (int[])route.Clone();  // shallow is OK
            int i = rnd.Next(0, n); int j = rnd.Next(0, n);
            int tmp = result[i];
            result[i] = result[j]; result[j] = tmp;
            return result;
        }

        static void Shuffle(int[] route, Random rnd)
        {
            // Fisher-Yates algorithm
            int n = route.Length;
            for (int i = 0; i < n; ++i)
      {
                int rIndx = rnd.Next(i, n);
                int tmp = route[rIndx];
                route[rIndx] = route[i];
                route[i] = tmp;
            }
        }

        static void ShowArray(int[] arr)
        {
            int n = arr.Length;
            Console.Write("[ ");
            for (int i = 0; i < n; ++i)
        Console.Write(arr[i].ToString().PadLeft(2) + " ");
            Console.WriteLine(" ]");
        }

        static int[] Solve(int nCities, Random rnd, int maxIter,
          double startTemperature, double alpha)
        {
            double currTemperature = startTemperature;
            int[] soln = new int[nCities];
            for (int i = 0; i < nCities; ++i) { soln[i] = i; }
            Shuffle(soln, rnd);
            Console.WriteLine("Initial guess: ");
            ShowArray(soln);

            double err = Error(soln);
            int iteration = 0;
            while (iteration < maxIter && err < 0.0)
      {
                int[] adjRoute = Adjacent(soln, rnd);
                double adjErr = Error(adjRoute);
                if (adjErr < err)  // better route 
        {
                    soln = adjRoute; err = adjErr;
                }
        else
                {
                    double acceptProb =
                      Math.Exp((err - adjErr) / currTemperature);
                    double p = rnd.NextDouble(); // corrected
                    if (p < acceptProb)  // accept anyway
          {
                        soln = adjRoute; err = adjErr;
                    }
                }

                if (currTemperature < 0.00001)
          currTemperature = 0.00001;
        else
                    currTemperature *= alpha;
                ++iteration;
            }  

            return soln;
        } 
    }
}
