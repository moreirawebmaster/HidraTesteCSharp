using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Infra.Data.EntityConfiguration
{
    public class ContaCorrenteConfig : IEntityTypeConfiguration<ContaCorrente>
    {
        public void Configure(EntityTypeBuilder<ContaCorrente> builder)
        {
            builder.ToTable("contas_corrente");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.Digito).HasColumnName("digito");
            builder.Property(x => x.NumeroConta).HasColumnName("numero");
            builder.Property(x => x.Saldo).HasColumnName("saldo").HasColumnType("decimal(18,2)");

            builder.HasMany(x => x.Extratos)
                .WithOne(x => x.ContaCorrente)
                .HasForeignKey(x => x.ContaCorrenteId)
                .HasConstraintName("fk_contas_corrente_extrato")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}