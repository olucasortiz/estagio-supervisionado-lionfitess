
using LionFitness.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;
using LionFitness.Middlewares;

namespace LionFitness
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Host.UseSerilog((context, services, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration)
                      .ReadFrom.Services(services);
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<ExceptionMiddleware>();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
