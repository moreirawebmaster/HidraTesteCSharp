using System.Collections.Generic;

namespace Bank.Commun.Domain.Entitie
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        private readonly List<string> _validates = new List<string>();
        public IReadOnlyList<string> Validades => _validates;
        protected void AddValidades(string validate) => _validates.Add(validate);
        public bool IsValid() => _validates.Count == 0;
    }
}