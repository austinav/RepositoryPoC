// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepositoryPoC.Contexts;

Console.WriteLine("Hello, World!");


var builder = Host.CreateApplicationBuilder();

builder.Services.AddDbContext<RepositoryPoCContext>(
    opt => opt.UseSqlServer(
        "data source=(localdb)\\mssqlserver01;initial catalog=Northwind;integrated security=True;App=EntityFramework")
    );

using IHost host = builder.Build(); 

await host.RunAsync();
