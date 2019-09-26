using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using Db;
using Domain.Network;
using NetworkUtils.Services;
using static System.Console;

namespace NetworkUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any())
            {
                WriteUsage();
                return;
            }

            if (!Enum.TryParse(args[0].Replace("--", ""), out Args enumValue))
            {
                WriteLine("Incorrect argument");
                WriteUsage();
                return;
            }

            switch (enumValue)
            {
                case Args.dhcp:
                    new DhcpConfigService().Generate();
                    break;
                case Args.nat:
                    new NatConfigService().Generate();
                    break;

                default:
                    WriteLine("Unknown Error");
                    WriteLine();
                    return;
            }

            WriteLine();
        }

        private static void WriteUsage()
        {
            WriteLine("Usage:");
            WriteLine($"--{Args.dhcp.ToString()} - generate dhcp config");
            WriteLine($"--{Args.nat.ToString()} - generate nat config");
            WriteLine($"--{Args.shaper.ToString()} - generate shaper config");
            WriteLine();
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Args
    {
        dhcp,
        nat,
        shaper
    }
}