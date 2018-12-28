using Bank.Domain.Entities;
using Bank.Infra.CrossCutting.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using NovaOperacao = Bank.Domain.Lancamentos.Commands.CriarNovaOperacao;

namespace Bank.Domain.Extratos.Events
{
    public class Event : INotificationHandler<NovaOperacao.Notification>
    {
        private readonly IRepository<Extrato> _repository;

        public Event(IRepository<Extrato> repository) => _repository = repository;

        public async Task Handle(NovaOperacao.Notification notification, CancellationToken cancellationToken)
        {
            await _repository.InsertAsync(Extrato.CriarExtrato(
                notification.TipoTransacao,
                notification.Id,
                notification.SaldoAnterior,
                notification.SaldoAtual));

            await _repository.SaveChangeAsync();
        }
    }
}