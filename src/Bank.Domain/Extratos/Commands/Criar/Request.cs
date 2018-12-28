using Bank.Commun.Domain.Dto;
using Bank.Domain.Entities;
using MediatR;

namespace Bank.Domain.Extratos.Commands.Criar
{
    public class Request : Validatable, IRequest<Result>
    {
        public int ContaCorrenteId { get;  set; }
        public TipoTransacao? TipoTransacao { get; set; } = null;
        public decimal SaldoAnterior { get;  set; }
        public decimal SaldoAtual { get;  set; }

        public override void Validate()
        {
            if(ContaCorrenteId <= 0)
                AddNotification(nameof(ContaCorrenteId), "Conta corrente inválida.");

            if(TipoTransacao == null)
                AddNotification(nameof(TipoTransacao), "Tipo de transação não informada.");

            if(SaldoAtual < SaldoAnterior)
                AddNotification(nameof(SaldoAtual), "Saldo atual não pode ser menor que o saldo anterior.");
        }

    }
}