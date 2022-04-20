using System;
using System.Diagnostics;

namespace OC_LAB03
{
    class Program
    {
        static void Main(string[] args)
        {
            float result, sum = 0, i, b, c;
        mark:
            Console.WriteLine("Введите i, b, c, искомой функции:");  
            try
            {
                i = float.Parse(Console.ReadLine());
                Console.WriteLine($"i:{i}");
                b = float.Parse(Console.ReadLine());
                Console.WriteLine($"b:{b}");
                c = float.Parse(Console.ReadLine());
                Console.WriteLine($"c:{c}");
            }
            catch
            {
                Console.WriteLine("Неверный ввод!");
                goto mark;
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int n2 = 1000;
            for (int n1 = 0; n1 < n2; n1++)
            {
                sum = b * 2 + c - i;
            }
            Console.WriteLine($"sum:{sum}");
            result = i + sum;
            Console.WriteLine($"f({i+1}):{result}");
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}