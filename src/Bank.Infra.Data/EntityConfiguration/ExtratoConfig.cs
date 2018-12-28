using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Infra.Data.EntityConfiguration
{
    public class ExtratoConfig : IEntityTypeConfiguration<Extrato>
    {
        public void Configure(EntityTypeBuilder<Extrato> builder)
        {

            builder.ToTable("extratos");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.ContaCorrenteId).HasColumnName("conta_corrente_id");
            builder.Property(x => x.DataTransacao).HasColumnName("data_transacao").ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.SaldoAnterior).HasColumnName("saldo_anterior").HasColumnType("decimal(18,2)");
            builder.Property(x => x.SaldoAtual).HasColumnName("saldo_atual").HasColumnType("decimal(18,2)");
            builder.Property(x => x.TipoTransacao).HasColumnName("tipo_transacao").HasColumnType("char(1)");
        }
    }
}