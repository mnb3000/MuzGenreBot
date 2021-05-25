using System;
using System.Threading;
using TelegramBot.Models;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            Bot.Initialize(cts).Wait();
            Console.WriteLine("Hello World!");
        }
    }
}