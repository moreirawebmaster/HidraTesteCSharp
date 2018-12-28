using System;
using Bank.Domain.Entities;
using MediatR;

namespace Bank.Domain.Lancamentos.Commands.CriarNovaOperacao
{
    public class Notification : INotification
    {
        public int Id { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public decimal Valor { get; set; }
        public decimal SaldoAtual { get; set; }
        public decimal SaldoAnterior { get; set; }
        
        public override string ToString() =>
            $"Foi realizada um operação de {TipoTransacao.ToString()}, na conta corrente de número {Id} tendo um saldo anterior R$: {SaldoAnterior}, passando a ter R$: {SaldoAtual}";
    }
}