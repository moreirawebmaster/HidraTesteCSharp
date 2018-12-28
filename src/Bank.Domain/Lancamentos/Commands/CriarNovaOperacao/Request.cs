using Bank.Commun.Domain.Dto;
using MediatR;

namespace Bank.Domain.Lancamentos.Commands.CriarNovaOperacao
{
    public class Request : Validatable, IRequest<Result>
    {
        public int ContaCorrenteDebito { get; set; }
        public int ContaCorrenteCredito { get; set; }
        public decimal Valor { get; set; }

        public override void Validate()
        {
            if(ContaCorrenteCredito == 0)
                AddNotification(nameof(ContaCorrenteCredito),"Número da conta corrente de credito inválido.");

            if (ContaCorrenteDebito == 0)
                AddNotification(nameof(ContaCorrenteCredito), "Número da conta corrente de debito inválido.");

            if (Valor <= 0)
                AddNotification(nameof(ContaCorrenteCredito), "Valor inválido");
        }
    }
}