using System;
using System.Collections;
using System.Collections.Generic;
namespace Lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Reflector.toFile(typeof(List<int>)));
        }
    }
}
