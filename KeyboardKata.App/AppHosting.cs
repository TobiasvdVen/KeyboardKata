﻿using KeyboardKata.App.ViewModels;
using KeyboardKata.Domain.Sessions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KeyboardKata.App
{
    public static class AppHosting
    {
        public static IHostBuilder AddKeyboardKata(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<MainViewModel>();

                services.AddSingleton<ITrainerSession>(s =>
                {
                    IConfiguration configuration = s.GetRequiredService<IConfiguration>();
                    string trainerPath = configuration["AppSettings:TrainerPath"] ?? throw new Exception("Trainer path not configured in AppSettings:TrainerPath!");

                    return new ProcessTrainerSession(trainerPath);
                });
            });

            return hostBuilder;
        }
    }
}
