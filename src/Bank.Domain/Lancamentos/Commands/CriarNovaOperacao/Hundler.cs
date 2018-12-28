using Bank.Commun.Domain.Dto;
using Bank.Domain.Entities;
using Bank.Infra.CrossCutting.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Bank.Domain.Lancamentos.Services;

namespace Bank.Domain.Lancamentos.Commands.CriarNovaOperacao
{
    public class Hundler : IRequestHandler<Request, Result>
    {
        private readonly ILancamentoServiceWrite _serviceWrite;

        public Hundler(ILancamentoServiceWrite serviceWrite) => _serviceWrite = serviceWrite;

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await _serviceWrite.CriarNovoLancamento(request, cancellationToken);
            return result;
        }
    }
}