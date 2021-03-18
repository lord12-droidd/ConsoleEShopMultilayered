using System;
using UI;

namespace ConsoleEShopMultilayered
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop processes = new Shop();
            processes.Start();
            Console.ReadKey();
        }
    }
}
