using System.Threading.Tasks;
using Bank.Commun.Domain.Dto;
using CriarNovaOperacao = Bank.Domain.Lancamentos.Commands.CriarNovaOperacao;

namespace Bank.Application.AppServices.Lancamentos
{
    public interface ILancamentoAppService
    {
        Task<Result> CriarNovaOperacao(CriarNovaOperacao.Request request);
    }
}