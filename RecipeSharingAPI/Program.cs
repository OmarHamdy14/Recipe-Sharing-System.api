using Microsoft.Extensions.DependencyInjection;
using RecipeSharingAPI.CloudinaryConfgs;
using RecipeSharingAPI.Helpers;
using RecipeSharingAPI.SignalrServices;

namespace RecipeSharingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

            builder.Services.AddSignalR();
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();   
            app.MapControllers();
            app.MapHub<SearchRecipeHub>("/SearchRecipeHub");

            app.Run();
        }
    }
}