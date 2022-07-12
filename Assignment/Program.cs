using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoResetEvent _handlerForOddNumber = new AutoResetEvent(true);
            AutoResetEvent _handlerForEvenNumber = new AutoResetEvent(false);

            var t1 = Task.Factory.StartNew(() =>
            {
                int[] arr = new int[] { 1, 3, 5, 7, 9, 11, 13, 15 };

                foreach (var item in arr)
                {
                    
                    Console.WriteLine(item);
                    _handlerForEvenNumber.Set();
                    _handlerForOddNumber.WaitOne();
                    
                }
            });
            var t2 = Task.Factory.StartNew(() =>
            {
                int[] arr = new int[] { 2, 4, 6, 8, 10, 12, 14 };

                foreach (var item in arr)
                {
                    Console.WriteLine(item);
                    _handlerForOddNumber.Set();
                    _handlerForEvenNumber.WaitOne();
                }
            });

            Console.WriteLine("End");
            Console.ReadLine();
        }

        static void PrintOddNumbers()
        {
            int[] arr = new int[] { 1, 3, 5, 7, 9, 11, 13, 15 };

            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }

        static void PrintEvenNumbers()
        {
            int[] arr = new int[] { 2, 4, 6, 8, 10, 12, 14 };

            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }

        }

    }
}