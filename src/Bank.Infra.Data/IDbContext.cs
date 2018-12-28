using System.Threading.Tasks;
using Bank.Commun.Domain.Entitie;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infra.Data
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : BaseEntity;
        EntityState Entry<T>(T entity) where T : BaseEntity;
        Task<int> SaveChangeAsync();
    }
}