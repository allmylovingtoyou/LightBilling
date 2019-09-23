using System;

namespace NetworkUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var dhcp = new DhcpConfigGenerator();
            dhcp.Generate();
            
            
            Console.WriteLine("End");
            Console.WriteLine();
        }
    }
}