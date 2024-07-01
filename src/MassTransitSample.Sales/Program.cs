using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;

var builder = WebHost.CreateDefaultBuilder(args);

var host = builder.Build();

await host.RunAsync();