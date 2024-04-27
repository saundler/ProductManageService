using Application.Service;
using FluentValidation;
using GrpcApi.Extensions;
using GrpcApi.Interceptor;
using Infrastructure.Repository;
using GrpcApi.Services;
    
namespace GrpcApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc(configureOption =>
        {
            configureOption.Interceptors.Add<GlobalExceptionHandlerInterceptor>();
            configureOption.Interceptors.Add<LoggingInterceptor>();
            configureOption.Interceptors.Add<ValidationInterceptor>();
        }).AddJsonTranscoding();
        builder.Services.AddGrpcSwagger();
        builder.Services.AddSwaggerGen();
        builder.Services.AddRepositories();
        builder.Services.AddServices();
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapGrpcService<ProductServiceGrpc>();
        
        app.Run();
    }
}