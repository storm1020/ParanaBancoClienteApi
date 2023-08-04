using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ParanaBancoClienteApi.Data;
using ParanaBancoClienteApi.Repositories;
using ParanaBancoClienteApi.Repositories.Interfaces;
using System.Reflection;

namespace ParanaBancoClienteApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ParanaBanco-ClienteApi",
                    Description = "ASP.NET Core Restfull Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Iago Rocha",
                        Url = new Uri("https://www.linkedin.com/in/iago-rocha-0a1837111/?msgControlName=null&msgConversationId=6696068456917282817&msgOverlay=true")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();

            builder.Services.AddEntityFrameworkSqlServer()
                    .AddDbContext<ParanabancoClienteDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );


            var app = builder.Build();

            app.UseSwagger();

            app.UseSwaggerUI();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(x => x.SerializeAsV2 = true);
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ParanaBanco-ClienteApi");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}