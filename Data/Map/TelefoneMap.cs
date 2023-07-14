using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParanaBancoClienteApi.Conversores;
using ParanaBancoClienteApi.Enums;
using ParanaBancoClienteApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParanaBancoClienteApi.Data.Map
{
    public class TelefoneMap : IEntityTypeConfiguration<TelefoneModel>
    {
        public void Configure(EntityTypeBuilder<TelefoneModel> builder)
        {
            builder.ToTable("TB_TELEFONE");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DDD)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(x => x.Numero)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.TipoTelefone)
                .IsRequired()
                .HasMaxLength(20)
                .HasConversion(new ConversorDeEnumParaString<TipoTelefone>());
        }
    }
}
