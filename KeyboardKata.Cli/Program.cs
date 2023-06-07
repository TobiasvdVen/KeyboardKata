﻿using KeyboardKata.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KeyboardKata.Cli
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args);

            builder.AddKeyboardKata<CliKeyboardKata>();

            builder.ConfigureServices(services =>
            {
                services.AddLogging(logging =>
                {
                    logging
                        .AddConsole()
                        .AddFilter("Microsoft", LogLevel.Warning)
                        .SetMinimumLevel(LogLevel.Warning);
                });
            });

            using IHost host = builder.Build();

            await host.RunAsync();
        }
    }
}
