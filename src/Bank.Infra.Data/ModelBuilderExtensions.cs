using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infra.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContaCorrente>().HasData(
                 ContaCorrente.NovaConta(0),
                 ContaCorrente.NovaConta(10),
                 ContaCorrente.NovaConta(50),
                 ContaCorrente.NovaConta(100)
            );
        }
    }
}