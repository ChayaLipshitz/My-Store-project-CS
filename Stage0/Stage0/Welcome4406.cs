using System;

namespace stage0
{
    partial class Program 
    { 
    static void Main(string[] args)
    {
            Welcome4406();
            Welcome7391();

            Console.ReadKey();
        }
        static partial void Welcome7391();
        private static void Welcome4406()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }

}
