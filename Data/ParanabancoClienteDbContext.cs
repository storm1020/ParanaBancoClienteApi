using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using ParanaBancoClienteApi.Conversores;
using ParanaBancoClienteApi.Data.Map;
using ParanaBancoClienteApi.Enums;
using ParanaBancoClienteApi.Models;

namespace ParanaBancoClienteApi.Data
{
    public class ParanabancoClienteDbContext : DbContext
    {
        public ParanabancoClienteDbContext(DbContextOptions<ParanabancoClienteDbContext> options) : base (options)
        {
        }

        public DbSet<ClienteModel> Clientes { get; set; }

        public DbSet<TelefoneModel> Telefones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());

            modelBuilder.ApplyConfiguration(new TelefoneMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
