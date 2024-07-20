using System.Data;
using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Para.Api.Middlewares;
using Para.Bussiness;
using Para.Bussiness.Cqrs;
using Para.Data.Context;

namespace Para.Api;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Para.Api", Version = "v1" });
        });

        var connectionStringSql = Configuration.GetConnectionString("MsSqlConnection");
        services.AddDbContext<ParaDbContext>(options => options.UseSqlServer(connectionStringSql));

        //services.AddScoped<IUnitOfWork, UnitOfWork>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        services.AddSingleton(config.CreateMapper());

        services.AddMediatR(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<RequestResponseLoggingMiddleware>();
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Para.Api v1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}