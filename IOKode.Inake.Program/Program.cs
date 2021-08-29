using System;
using ConsoleAppFramework;
using IOKode.Inake;
using IOKode.Inake.Program;
using IOKode.Inake.RandomGenerators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddTransient<IRandomGenerator>(_ => new SystemRandomGenerator(new Random()));
        services.AddTransient<WordGenerator>();
    })
    .RunConsoleAppFrameworkAsync<GenerateCommand>(args);