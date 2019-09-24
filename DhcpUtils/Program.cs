using System;

namespace NetworkUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("#Start dhcp config generator");
            Console.WriteLine();
            var dhcp = new DhcpConfigGenerator();
            dhcp.Generate();

            Console.WriteLine();
            Console.WriteLine("#End dhcp config generator");
        }
    }
}