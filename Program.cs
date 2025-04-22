using SmartReplySystem.Interfaces;
using SmartReplySystem.Services;
using Microsoft.OpenApi.Models;

namespace SmartReplySystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddScoped<IAiService, AiService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddSwaggerGen(c => 
            {    
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartReplySystem API", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartReplySystem API v1");
                });
            }

            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
