using Autofac;
using Autofac.Extensions.DependencyInjection;
using Para.Bussiness.DependencyResolvers.Autofac;

namespace Para.Api;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>((context, builder) =>
        {
            var configuration = context.Configuration;
            builder.RegisterModule(new AutofacBusinessModule(configuration));
        })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}