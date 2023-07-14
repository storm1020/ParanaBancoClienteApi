using Microsoft.EntityFrameworkCore;
using ParanaBancoClienteApi.Data;
using ParanaBancoClienteApi.Repositories;
using ParanaBancoClienteApi.Repositories.Interfaces;

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
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<ParanabancoClienteDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}