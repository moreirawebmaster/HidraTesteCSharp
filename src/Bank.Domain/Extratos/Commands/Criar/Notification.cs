using Bank.Domain.Entities;
using System;
using MediatR;

namespace Bank.Domain.Extratos.Commands.Criar
{
    public class Notification : INotification
    {
        public int ContaCorrenteId { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoAtual { get; set; }
        public DateTime DataExtrato { get; set; }

        public override string ToString() =>
            $"Foi realizada um operação de {TipoTransacao.ToString()}, na conta corrente de número {ContaCorrenteId} tendo um salado anterior R$: {SaldoAnterior}, passando a ter R$: {SaldoAtual} no dia {DataExtrato:d} as {DataExtrato:HH:mm}";
    }
}