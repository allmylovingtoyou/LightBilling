using System;
using Microsoft.Extensions.Logging;
using tik4net;

namespace NetworkUtils.Utils
{
    public class RouterUtils
    {
        private readonly ILogger _logger;

        public RouterUtils(ILogger logger)
        {
            _logger = logger;
        }

        public void Connection_OnWriteRow(object sender, TikConnectionCommCallbackEventArgs args)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            _logger.LogDebug(">" + args.Word);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void Connection_OnReadRow(object sender, TikConnectionCommCallbackEventArgs args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            _logger.LogDebug("<" + args.Word);
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}