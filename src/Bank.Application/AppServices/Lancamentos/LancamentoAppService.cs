using System.Threading;
using System.Threading.Tasks;
using Bank.Commun.Domain.Dto;
using Bank.Domain.Lancamentos.Commands.CriarNovaOperacao;
using MediatR;

namespace Bank.Application.AppServices.Lancamentos
{
    public class LancamentoAppService : ILancamentoAppService
    {
        private readonly IMediator _mediator;

        public LancamentoAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result> CriarNovaOperacao(Request request)
        {
            var result = await _mediator.Send(request, CancellationToken.None);
            return result;
        }
    }
}