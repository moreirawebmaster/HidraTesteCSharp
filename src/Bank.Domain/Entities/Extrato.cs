using Bank.Commun.Domain.Entitie;
using System;

namespace Bank.Domain.Entities
{
    public class Extrato : BaseEntity
    {
        public int ContaCorrenteId { get; protected set; }
        public TipoTransacao TipoTransacao { get; protected set; }
        public decimal SaldoAnterior { get; protected set; }
        public decimal SaldoAtual { get; protected set; }
        public DateTime DataTransacao { get; protected set; }
        public virtual ContaCorrente ContaCorrente { get; protected set; }

        public static Extrato CriarExtrato(TipoTransacao tipoTransacao, int contaCorrenteId, decimal saldoAnterior, decimal saldoAtual)
        {
            var extrato = new Extrato { TipoTransacao = tipoTransacao, ContaCorrenteId = contaCorrenteId };
            return extrato;
        }

    }

    public enum TipoTransacao
    {
        Debito = 'D',
        Credito = 'C'
    }
}