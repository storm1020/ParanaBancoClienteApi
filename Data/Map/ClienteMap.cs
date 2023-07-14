using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParanaBancoClienteApi.Conversores;
using ParanaBancoClienteApi.Enums;
using ParanaBancoClienteApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParanaBancoClienteApi.Data.Map
{
    public class ClienteMap : IEntityTypeConfiguration<ClienteModel>
    {
        public void Configure(EntityTypeBuilder<ClienteModel> builder)
        {
            builder.ToTable("TB_CLIENTE");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Sobrenome).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);

            builder.HasMany(x => x.Telefones)
                .WithOne(t => t.Cliente)
                .HasForeignKey(t => t.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
