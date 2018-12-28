using Bank.Commun.Domain.Entitie;
using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Bank.Infra.Data
{
    public class BankContext : DbContext, IDbContext
    {
        //Para utilizar a migration
        public DbSet<ContaCorrente> ContasCorrente { get; set; }
        public DbSet<Extrato> Extratos { get; set; }

        public new DbSet<T> Set<T>() where T : BaseEntity => base.Set<T>();

        public new EntityState Entry<T>(T entity) where T : BaseEntity =>
            base.Entry(entity).State = EntityState.Modified;

        public Task<int> SaveChangeAsync() => base.SaveChangesAsync();
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesConfiguration = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(gi =>
                    gi.IsGenericType &&
                    gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();


            foreach (var type in typesConfiguration)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            var strings = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string));

            foreach (var property in strings)
            {
                if (property.Relational().ColumnType == null)
                    property.Relational().ColumnType = "varchar";

                if (property.GetMaxLength() == null)
                    property.SetMaxLength(200);
            }

            //modelBuilder.Seed();
        }
    }
}