using System;
using System.Collections;
using System.Collections.Generic;
namespace Lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Reflector.toFile(typeof(List<int>), typeof(int), "IntListInfo.txt") + "\n");
            Console.WriteLine(Reflector.toFile(typeof(string), typeof(string), "StringInfo.txt") + "\n");
            Console.WriteLine(Reflector.toFile(typeof(CList<char>), typeof(char), "CharCListInfo.txt") + "\n");
            Console.WriteLine(Reflector.toFile(typeof(Logger), typeof(Exception), "LoggerInfo.txt") + "\n");
            List<int> intList = (List<int>)Reflector.Create(typeof(List<int>), new int[] {10, 12, 13});
            foreach(int number in intList)
            {
                Console.Write(number + " ");
            }

        }
    }
}
